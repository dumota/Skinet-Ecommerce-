namespace Skinet_API.Errors
{
    public class ApiValidationErroResponse : ApiResponse
    {
        public ApiValidationErroResponse() : base(400)
        {
        }

        public IEnumerable<String> Errors { get; set; }
    }
}
