﻿using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace ShakespearePokemon.Tests.Unit
{
    public class PokemonServiceTests
    {
        [Test]
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
        public void ExtractDescription_ReturnsTheWhitelistedDescription_GivenAVersionFilter()
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
        public void ExtractVersionNumber_ReturnsTheVersionNumber_GivenAVersionUrl()
        {
            int version = PokemonService.ExtractVersionNumber(new Version { Url = "https://pokeapi.co/api/v2/version/10/" });

            Assert.AreEqual(10, version);
        }

        [Test]
        public void RemoveSpecialCharacters_Replace_GivenATextContaingNewLines()
        {
            Assert.AreEqual("This is a line. This is another one with a tab inside.",
                PokemonService.ReplaceSpecialCharactersWithSpaces("This is a line.\r\nThis is another one with a tab\tinside."));
        }
    }
}