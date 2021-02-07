
# Shakespeare Pokemon REST API

![build status](https://github.com/lucamazzanti/shakespeare-pokemon/workflows/build/badge.svg)
![release status](https://github.com/lucamazzanti/shakespeare-pokemon/workflows/release/badge.svg)
[![coverage](https://codecov.io/gh/lucamazzanti/shakespeare-pokemon/branch/main/graph/badge.svg?token=KYXB2QARGD)](https://codecov.io/gh/lucamazzanti/shakespeare-pokemon/)
[![maintainability](https://api.codeclimate.com/v1/badges/aa4184097448ea8ef964/maintainability)](https://codeclimate.com/github/lucamazzanti/shakespeare-pokemon/maintainability)
[![Microsoft Naming Guidelines](https://img.shields.io/badge/code%20style-Microsoft-brightgreen.svg?style=flat)](https://www.jetbrains.com/help/resharper/2020.3/InconsistentNaming.html "Microsoft codestyle")
[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Flucamazzanti%2Fshakespeare-pokemon&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
[![license: GPL v3](https://img.shields.io/badge/license-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
[![Known Vulnerabilities](https://snyk.io/test/github/lucamazzanti/shakespeare-pokemon/badge.svg?targetFile=ShakespearePokemon.API/ShakespearePokemon.API.csproj)](https://snyk.io/test/github/lucamazzanti/shakespeare-pokemon?targetFile=ShakespearePokemon.API/ShakespearePokemon.API.csproj)

![shakespeare-pokemon-logo](https://github.com/lucamazzanti/shakespeare-pokemon/blob/main/.docs/pokemon-shakespeare-small.png)

> What if the description of each Pokemon were to be written using Shakespeare's style?

This is a REST API that, given a Pokemon name, returns its Shakespearean description.

## API Specification

Signature: `GET endpoint: /pokemon/<pokemon name>/`

Example: http://localhost:5000/pokemon/charizard/

Output:
```json
{
	"name": "charizard",
	"description": "Charizard flies 'round the sky in search of powerful opponents. 
		't breathes fire of such most wondrous heat yond 't melts aught. However,  
		't nev'r turns its fiery breath on any opponent weaker than itself."
}
```

# How to run

Download the Linux image from [Docker Hub](https://hub.docker.com/) and run it:

```bash?line_numbers=false
docker pull lmazzanti/shakespeare-pokemon
docker run -d -p 5000:80 --name shakespeare-pokemon lmazzanti/shakespeare-pokemon
```

If you have an authentication token from [Fun Translations](https://funtranslations.com/api/shakespeare/) you can pass it as:

```bash?line_numbers=false
docker run -d -p 5000:80 -e "API:Shakespeare:AuthenticationToken=xxxx" --name shakespeare-pokemon shakespeare-pokemon
```

Go to http://localhost:5000 in a browser to test the app.

See [Docker Hub repository](https://hub.docker.com/repository/docker/lmazzanti/shakespearepokemonapi/) for more details.

See [Docker run command](https://docs.docker.com/engine/reference/run/) form more details on how to run it.

# How to build

Here the steps to build and run locally the program.

## Prerequisites

- [.NET SDK 5.0](https://dotnet.microsoft.com/download/)
- Docker client 18.03 or later ([CentOS](https://docs.docker.com/install/linux/docker-ce/centos/), [Debian](https://docs.docker.com/install/linux/docker-ce/debian/), [Fedora](https://docs.docker.com/install/linux/docker-ce/fedora/), [Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/), [macOS](https://docs.docker.com/docker-for-mac/install/), [Windows](https://docs.docker.com/docker-for-windows/install/))
- [Git](https://git-scm.com/download/)

## Download source code

Download the source manually or clone the Git repository:

```bash?line_numbers=false
git clone https://github.com/lucamazzanti/shakespeare-pokemon
```

## Run the application locally

Navigate to the project folder at \ShakespearePokemon.API.

Run the following command to build and run the app locally:

```bash?line_numbers=false
dotnet run
```

Go to http://localhost:5000 in a browser to test the app.

Press Ctrl+C at the command prompt to stop the app.

## Run in a Linux container

In the Docker client, switch to Linux containers.

Navigate to the project folder at \ShakespearePokemon.API.

Run the following commands to build and run the app in Docker:

```bash?line_numbers=false
docker build -f Dockerfile -t shakespeare-pokemon ..
docker run -it --rm -p 5000:80 --name shakespeare-pokemon shakespeare-pokemon
```

The build command arguments:
- Name the image shakespeare-pokemon.
- Run the Dockerfile working from the parent folder (the double period at the end).

The run command arguments:
- Allocate a pseudo-TTY
- Automatically remove the container when it exits.
- Map port 5000 on the local machine to port 80 in the container.
- Name the container shakespeare-pokemon.
- Specify the shakespeare-pokemon image.

If you have an authentication token from [Fun Translations](https://funtranslations.com/api/shakespeare/) you can pass it as:

```bash?line_numbers=false
docker run -it --rm -p 5000:80 -e "API:Shakespeare:AuthenticationToken=xxxx" --name shakespeare-pokemon shakespeare-pokemon
```

Go to http://localhost:5000 in a browser to test the app.

# Project details

This is a case study of a RESTful API made in ASP NET Core.

It consumes 2 external API and produces a new result.
  - The pokemon API asks caching as requirement.
  - The translation API has an heavy rate limiting of 5 calls\hour without an authentication token.
  
The project structure choosen for that tiny example avoid over splitting into projects:
- A single Web Api 5.0 project, layers are divided in folders.
- A single NUnit test project holding all the categories: unit, integration, functional tests.

The project can be run in a linux docker container, hosted on a web server or self-hosted.

The project was developed using the github management lifecycle, see [here](https://github.com/lucamazzanti/shakespeare-pokemon/projects/1) for tasks and more considerations.

Github actions runs the CI/CD.

## External API

- [PokéAPI](https://pokeapi.co/)
- [Fun Translations](https://funtranslations.com/api/shakespeare/)
