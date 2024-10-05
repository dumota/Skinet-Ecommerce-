using Microsoft.EntityFrameworkCore;
using Skinet_API.Extensions;
using Skinet_API.Helpers;
using Skinet_API.Middleware;
using Skinet_Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAplicationServices();
builder.Services.AddSwaggerDocumentations();
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});




var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    
    using (var scope = app.Services.CreateScope())
    {
        app.UseSwaggerDocumentation();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await StoreContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}
//app.UseCors("CorsPolicy");

app.UseCors("AllowSpecificOrigin");
// Seeding the database

//adicionando um middleware para tratar as exceptions da requisições
app.UseMiddleware<ExceptionMiddleware>();
//adicionando a api de erros para caso de erros do protocolo http, redirecionando para capturar estas 
//mensagens -> MIDDLEWARE

app.UseStatusCodePagesWithReExecute("/erros/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
