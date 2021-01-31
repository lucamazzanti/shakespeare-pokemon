#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["ShakespearePokemon.API/ShakespearePokemon.API.csproj", "ShakespearePokemon.API/"]
RUN dotnet restore "ShakespearePokemon.API/ShakespearePokemon.API.csproj"
COPY . .
WORKDIR "/src/ShakespearePokemon.API"
RUN dotnet build "ShakespearePokemon.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShakespearePokemon.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShakespearePokemon.API.dll"]