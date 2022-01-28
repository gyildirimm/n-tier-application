using Core.Utilities.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionDataResultInstance<T>(IDataResult<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        public IActionResult ActionResultInstance(Core.Utilities.Wrappers.IResult response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

    }
}
