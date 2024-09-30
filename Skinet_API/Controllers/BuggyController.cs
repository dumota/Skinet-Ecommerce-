using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet_API.Errors;
using Skinet_Infrastructure.Data;

namespace Skinet_API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetnotFoundRequest()
        {
            var thing = _storeContext.Products.Find(43);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _storeContext.Products.Find(43);
            var thingReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadFoundRequest(int id)
        {
            return Ok();
        }
    }
}
