using Moq;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using ShakespearePokemon.Tests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Unit
{
    // Unit tests on the Pokemon Service
    public class PokemonServiceTest : UnitTest
    {
        [Test]
        [Description("ExtractDescription filters the flavor texts keeping only the english languages.")]
        public void ExtractDescription_ReturnsEnglishDescription_GivenTwoLanguages()
        {
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "red-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    },
                    new()
                    {
                        FlavorText = "red-it",
                        Language = new Language { Name = "it" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    }
                }
            };

            var pokemonService = new PokemonService(null);

            string descrpition = pokemonService.ExtractDescription(pokemonSpecies);

            Assert.AreEqual("red-en", descrpition);
        }

        [Test]
        [Description("ExtractDescription filters the flavor texts keeping only the latest version available.")]
        public void ExtractDescription_ReturnsTheLastVersionedDescription_GivenMultipleVersions()
        {
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "red-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    },
                    new()
                    {
                        FlavorText = "blue-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "blue", Url = "https://pokeapi.co/api/v2/version/2/" }
                    },
                    new()
                    {
                        FlavorText = "pearl-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "pearl", Url = "https://pokeapi.co/api/v2/version/13/" }
                    }
                }
            };

            var pokemonService = new PokemonService(null);

            string descrpition = pokemonService.ExtractDescription(pokemonSpecies);

            Assert.AreEqual("pearl-en", descrpition);
        }

        [Test]
        [Description("ExtractDescription filters the flavor texts keeping only the versions if in whitelist.")]
        public void ExtractDescription_ReturnsTheWhitelistedDescription_GivenVersionFilter()
        {
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "red-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    },
                    new()
                    {
                        FlavorText = "blue-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "blue", Url = "https://pokeapi.co/api/v2/version/2/" }
                    }
                }
            };

            var pokemonService = new PokemonService(null) { FilteredVersions = new[] { "blue" } };

            string descrpition = pokemonService.ExtractDescription(pokemonSpecies);

            Assert.AreEqual("blue-en", descrpition);
        }

        [Test]
        [Description("ExtractDescription doesn't filter the flavor texts by versions if the whitelist is empty.")]
        public void ExtractDescription_ReturnsTheDescription_GivenAnEmptyVersionFilter()
        {
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "red-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    }
                }
            };

            var pokemonService = new PokemonService(null) { FilteredVersions = System.Array.Empty<string>() };

            string descrpition = pokemonService.ExtractDescription(pokemonSpecies);

            Assert.AreEqual("red-en", descrpition);
        }

        [Test]
        [Description("Returns the version number given a version resource url.")]
        public void ExtractVersionNumber_ReturnsTheVersionNumber_GivenVersionUrl()
        {
            int version = PokemonService.ExtractVersionNumber(new Version { Url = "https://pokeapi.co/api/v2/version/10/" });

            Assert.AreEqual(10, version);
        }

        [Test]
        [Description("Replace all the spatial characters to keep the description in single line.")]
        public void ReplaceSpecialCharactersWithSpaces_Replace_GivenTextContaingNewLines()
        {
            Assert.AreEqual("This is a line. This is another one with a tab inside.",
                PokemonService.ReplaceSpecialCharactersWithSpaces("This is a line.\r\nThis is another one with a tab\tinside."));
        }

        [Test]
        [Description("Returns the pokemon description given an existing name.")]
        public async Task GetPokemonDescriptionAsync_ReturnsDescription_GivenExistentName()
        {
            // Arrange a response from the pockemon client
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "red-en",
                        Language = new Language { Name = "en" },
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                    }
                }
            };

            // mocking pockemon client
            var client = new Mock<IPokemonClient>();
            client.Setup(i => i.GetPokemonSpeciesAsync(It.IsAny<string>()))
                .ReturnsAsync(pokemonSpecies);
            var service = new PokemonService(client.Object);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("charizard");

            var expected = new PokemonDescription
            {
                Name = "charizard",
                Description = "red-en"
            };
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Description, result.Description);
        }

        [Test]
        [Description("Returns null when given an unknown name.")]
        public async Task GetPokemonDescriptionAsync_ReturnsNull_GivenUnknownName()
        {
            // the empty mock will return null when called
            var client = new Mock<IPokemonClient>();
            var service = new PokemonService(client.Object);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("charizard");

            Assert.IsNull(result);
        }
    }
}
