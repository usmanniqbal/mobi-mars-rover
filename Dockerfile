FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

RUN apt-get update && apt-get install make

WORKDIR /app

COPY . ./

RUN dotnet build ./Mars-Rover/Mars-Rover.csproj

RUN dotnet build ./Mars-Rover.Tests/Mars-Rover.Tests.csproj

ENTRYPOINT [ "make" ]