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
	"description": "...."
}
```

## Prerequisites
- [.NET SDK 5.0](https://dotnet.microsoft.com/download ".NET SDK 5.0")
- Docker client 18.03 or later ([CentOS](https://docs.docker.com/install/linux/docker-ce/centos/ "CentOS"), [Debian](https://docs.docker.com/install/linux/docker-ce/debian/ "Debian"), [Fedora](https://docs.docker.com/install/linux/docker-ce/fedora/ "Fedora"), [Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/ "Ubuntu"), [macOS](https://docs.docker.com/docker-for-mac/install/ "macOS"), [Windows](https://docs.docker.com/docker-for-windows/install/ "Windows"))
- [Git](https://git-scm.com/download "Git")

## How to run
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
docker build -t shakespeare-pokemon ..
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

Go to http://localhost:5000 in a browser to test the app.

### Troubleshooting
Before open an issue or contact me, please check these useful resources:
- [ASP NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals "ASP NET Core")
- [ASP.NET Core Runtime](https://hub.docker.com/_/microsoft-dotnet-aspnet "ASP.NET Core Runtime")
- [Docker images for ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images "Docker images for ASP.NET Core")
- [How Visual Studio builds containerized apps](https://docs.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2019 "How Visual Studio builds containerized apps")

## Project details
In progress.

### External API
- https://pokeapi.co/
- https://funtranslations.com/api/shakespeare

### Improvements
In progress.
