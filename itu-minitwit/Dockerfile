FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# base docker image on image with .NET core runtime - has needed dependencies
# https://medium.com/@aliyildizoz/understanding-asp-net-core-dockerfile-a523233bb9a4 <- guide
# https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux&pivots=dotnet-8-0 <-guide
# https://hub.docker.com/r/microsoft/dotnet-runtime <- dockers

# sdk needed?
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# copy the solution, tests and projects
COPY Chirp.sln Chirp.sln
RUN mkdir src
RUN mkdir test
# https://stackoverflow.com/questions/74120448/understanding-this-docker-file-where-are-the-files-being-copied
COPY ./test test/ 
COPY ./src/ src/

RUN dotnet restore

# set work directory to chirp.web - so we can run dotnet run
WORKDIR /src/Chirp.Web/

RUN dotnet run