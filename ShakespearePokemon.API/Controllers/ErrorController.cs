using System;
using System.Net.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
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

            return HandleException(context.Error) ?? Problem();
        }

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return HandleException(context.Error) ?? Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }

        private ObjectResult HandleException(Exception ex)
        {
            if (ex is HttpRequestException exception)
            {
                HttpRequestException httpRequestException = exception;

                // external consumed API server errrors will be converted in ServiceUnavailable or maybe in future in BadGateway
                if (httpRequestException.StatusCode != null && (int)httpRequestException.StatusCode >= 500)
                {
                    return Problem(statusCode: StatusCodes.Status503ServiceUnavailable);
                }

                // external consumed API server rate limiting error will be rethrown as ours
                if (httpRequestException.StatusCode != null && (int)httpRequestException.StatusCode == 429)
                {
                    return Problem(statusCode: StatusCodes.Status429TooManyRequests, detail: "Rate limit exceeded.");
                }
            }

            return null;
        }
    }
}
