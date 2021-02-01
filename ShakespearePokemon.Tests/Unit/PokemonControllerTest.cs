using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShakespearePokemon.API.Controllers;
using ShakespearePokemon.API.Models;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using ShakespearePokemon.Tests.Common;
using System.Net;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Unit
{
    // Unit tests on the Pokemon Controller outside the middleware context.
    public class PokemonControllerTest : UnitTest
    {
        [Test]
        [Description("When called GetPokemonAsync on an existent species it returns the name an description in Shakespeare words.")]
        public async Task GetPokemonAsync_ReturnsOk_GivenExistentName()
        {
            // the service returns a sample description for "charizard"
            var expectedResult = new ShakespearePokemonDescription { Name = "charizard", Description = "sample description"};
            var pokemonService = new Mock<IShakespearePokemonService>();
            pokemonService
                .Setup(i => i.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            var controller = new PokemonController(pokemonService.Object);

            ActionResult<PokemonViewModel> result = await controller.GetPokemonAsync("charizard");

            Assert.That(result, Is.TypeOf<ActionResult<PokemonViewModel>>());
            Assert.AreEqual(expectedResult.Name, result.Value.Name);
            Assert.AreEqual(expectedResult.Description, result.Value.Description);
        }

        [Test]
        [Description("When called GetPokemonAsync on an unknown species it returns an error 404.")]
        public async Task GetPokemonAsync_ReturnsNotFound_GivenUnknownName()
        {
            // the service returns null when asked for a pokemon description
            var pokemonService = new Mock<IShakespearePokemonService>();

            var controller = new PokemonController(pokemonService.Object);

            ActionResult<PokemonViewModel> result = await controller.GetPokemonAsync("charizard");

            MvcAssert.IsProblemErrorResult(result, HttpStatusCode.NotFound);
        }
    }
}
