using System.Threading.Tasks;
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
        public async Task GetPokemon_ReturnsOK_GivenExistentName()
        {
            // Arrange
            // the service returns a sample pokemon
            var serviceResult = new ShakespearePokemonDescription { Name = "charizard", Description = "sample description"};
            var pokemonService = new Mock<IShakespearePokemonService>();
            pokemonService
                .Setup(i => i.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(serviceResult);

            var controller = new PokemonController(pokemonService.Object);

            // Act
            ActionResult<PokemonViewModel> result = await controller.GetPokemonAsync("charizard");

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<PokemonViewModel>>());
            Assert.AreEqual(serviceResult.Name, result.Value.Name);
            Assert.AreEqual(serviceResult.Description, result.Value.Description);
        }

        [Test]
        public async Task GetPokemon_ReturnsNotFound_GivenNotFoundName()
        {
            // Arrange
            // the service returns null
            var pokemonService = new Mock<IShakespearePokemonService>();

            var controller = new PokemonController(pokemonService.Object);

            // Act
            ActionResult<PokemonViewModel> result = await controller.GetPokemonAsync("charizard");

            // Assert
            MvcAssert.IsProblemErrorResult(result, StatusCodes.Status404NotFound);
        }
    }
}
