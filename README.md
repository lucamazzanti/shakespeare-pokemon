![shakespeare-pokemon-logo](https://github.com/lucamazzanti/shakespeare-pokemon/blob/main/.docs/pokemon-shakespeare-319x305.png)

# Shakespeare Pokemon REST API
> What if the description of each Pokemon were to be written using Shakespeare's style?

This is a REST API that, given a Pokemon name, returns its Shakespearean description.

## API Specification

Signature: `GET endpoint: /pokemon/<pokemon name>`

Example: http://localhost:5000/pokemon/charizard

Output:
```json
{
	"name": "charizard",
	"description": "Charizard flies 'round the sky in search of powerful opponents. 
		't breathes fire of such most wondrous heat yond 't melts aught. However,  
		't nev'r turns its fiery breath on any opponent weaker than itself."
}
```

## Prerequisites
- [.NET SDK 5.0](https://dotnet.microsoft.com/download ".NET SDK 5.0")
- Docker client 18.03 or later ([CentOS](https://docs.docker.com/install/linux/docker-ce/centos/ "CentOS"), [Debian](https://docs.docker.com/install/linux/docker-ce/debian/ "Debian"), [Fedora](https://docs.docker.com/install/linux/docker-ce/fedora/ "Fedora"), [Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/ "Ubuntu"), [macOS](https://docs.docker.com/docker-for-mac/install/ "macOS"), [Windows](https://docs.docker.com/docker-for-windows/install/ "Windows"))
- [Git](https://git-scm.com/download "Git")

## How to run from Docker Hub
Download the Linux image from Docker Hub and run it:

```bash?line_numbers=false
docker pull lmazzanti/shakespearepokemonapi
docker run -it --rm -p 5000:80 --name shakespeare-pokemon-api shakespearepokemonapi
```

See [here](https://hub.docker.com/repository/docker/lmazzanti/shakespearepokemonapi) for more details.

## How to build and run
Download the source manually or clone the Git repository:
```bash?line_numbers=false
git clone https://github.com/lucamazzanti/shakespeare-pokemon
```
### Run the application locally
Navigate to the project folder at \ShakespearePokemon.API.

Run the following command to build and run the app locally:

```bash?line_numbers=false
dotnet run
```
Go to http://localhost:5000 in a browser to test the app.

Press Ctrl+C at the command prompt to stop the app.

### Run in a Linux container
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

If you have an authentication token from [funtranslations](https://funtranslations.com/api/shakespeare) you can pass it as:

```bash?line_numbers=false
docker run -it --rm -p 5000:80 -e "API:Shakespeare:AuthenticationToken=xxxx" --name shakespeare-pokemon shakespeare-pokemon
```

Go to http://localhost:5000 in a browser to test the app.

## Project details
This an example of an ASP NET Core Web application 5.0, it consumes 2 external API and produces a new result.

The boilerplate structure shows some of the aspects you can find that type of project.

The pokemon API requires caching, the Shakespeare API give us heavy rate limiting without an authentication token.

I setup a client cache on the endpoint, a local in memory server cache on the pokemon service, a configuration that accept the token for the other one.

A single test project shows how can be tested the project: unit, integration, functional tests.

The project can be run in a linux docker container, hosted on a web server or self-hosted.

The project was developed using the github management lifecycle, see [here](https://github.com/lucamazzanti/shakespeare-pokemon/projects/1) for tasks and considerations.

### External API
- https://pokeapi.co/
- https://funtranslations.com/api/shakespeare

## Improvements
See all the tasks in todo, here the missing steps:
- Continuous integration in GitHub actions
- Deploy on Docker Hub at the end of CI\CD
- healthcheks for service discovery and docker
- telemetry for kubernetes

### Troubleshooting
Before open an issue or contact me, please check these useful resources:
- [ASP NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals "ASP NET Core")
- [ASP.NET Core Runtime](https://hub.docker.com/_/microsoft-dotnet-aspnet "ASP.NET Core Runtime")
- [Docker images for ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images "Docker images for ASP.NET Core")
- [How Visual Studio builds containerized apps](https://docs.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2019 "How Visual Studio builds containerized apps")
