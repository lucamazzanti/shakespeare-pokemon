using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ShakespearePokemon.Tests.Common.BaseTests
{
    [Category(TestCategories.Integration)]
    public abstract class IntegrationTest : BaseTest
    {
        protected static IConfiguration Configuration { get; }

        static IntegrationTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", false)
                .Build();
        }
    }
}
