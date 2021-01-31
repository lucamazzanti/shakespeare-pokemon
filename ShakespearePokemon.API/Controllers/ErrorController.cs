using System.Net.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShakespearePokemon.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is HttpRequestException exception)
            {
                HttpRequestException httpRequestException = exception;

                // external consumed API server errrors will be converted in ServiceUnavailable or maybe in future in BadGateway
                if (httpRequestException.StatusCode != null && (int) httpRequestException.StatusCode >= 500)
                {
                    return Problem(statusCode: StatusCodes.Status503ServiceUnavailable);
                }
            }

            return Problem();
        }
    }
}
