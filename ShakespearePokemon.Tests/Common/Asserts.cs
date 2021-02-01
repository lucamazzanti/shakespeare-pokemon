using System.Net;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace NUnit.Framework
{
    /// <summary>
    /// Asserts in support of ASP NET MVC\WebAPI framework.
    /// </summary>
    public static class MvcAssert
    {
        /// <summary>
        /// Validate the ActionResult when the Value is not returned and the status code is the expected.
        /// </summary>
        /// <typeparam name="T">The type of Value.</typeparam>
        /// <param name="result">The result to validate.</param>
        /// <param name="expectedStatusCode">The expexted HttpStatusCode.</param>
        public static void IsProblemErrorResult<T>(ActionResult<T> result, HttpStatusCode expectedStatusCode)
        {
            // No value returned
            Assert.IsNull(result.Value);

            // An ObjectResult containing the ProblemDetails with the expected status code
            Assert.That(result.Result, Is.TypeOf<ObjectResult>().
                With.Property("StatusCode")
                .EqualTo((int) expectedStatusCode));
        }
    }
}
