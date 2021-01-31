using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShakespearePokemon.API.Controllers;
using ShakespearePokemon.API.Models;
using ShakespearePokemon.API.Services.ShakespearePokemon;

namespace ShakespearePokemon.Tests.Unit
{
    public class PokemonControllerTests
    {
        [Test]
        public void GetPokemon_ReturnsOK_GivenExistentName()
        {
            // Arrange
            // the service returns a sample pokemon
            var serviceResult = new ShakespearePokemonDescription { Name = "charizard", Description = "sample description"};
            var pokemonService = new Mock<IShakespearePokemonService>();
            pokemonService
                .Setup(i => i.GetPokemon(It.IsAny<string>()))
                .Returns(serviceResult);

            var controller = new PokemonController(pokemonService.Object);

            // Act
            ActionResult<PokemonViewModel> result = controller.GetPokemon("charizard");

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<PokemonViewModel>>());
            Assert.AreEqual(serviceResult.Name, result.Value.Name);
            Assert.AreEqual(serviceResult.Description, result.Value.Description);
        }

        [Test]
        public void GetPokemon_ReturnsNotFound_GivenNotFoundName()
        {
            // Arrange
            // the service returns null
            var pokemonService = new Mock<IShakespearePokemonService>();

            var controller = new PokemonController(pokemonService.Object);

            // Act
            ActionResult<PokemonViewModel> result = controller.GetPokemon("charizard");

            // Assert
            MvcAssert.IsProblemErrorResult(result, StatusCodes.Status404NotFound);
        }
    }
}
