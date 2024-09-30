using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Skinet_API.Errors;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Skinet_API.Controllers
{
    [Route("erros/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
