using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace NUnit.Framework
{
    public static class MvcAssert
    {
        public static void IsProblemErrorResult<T>(ActionResult<T> result, int statusCode)
        {
            Assert.IsNull(result.Value);
            Assert.That(result.Result, Is.TypeOf<ObjectResult>().
                With.Property("StatusCode")
                .EqualTo(statusCode));
        }
    }
}
