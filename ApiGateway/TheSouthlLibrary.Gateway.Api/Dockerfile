#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiGateway/TheSouthlLibrary.Gateway.Api/TheSouthlLibrary.Gateway.Api.csproj", "ApiGateway/TheSouthlLibrary.Gateway.Api/"]
RUN dotnet restore "ApiGateway/TheSouthlLibrary.Gateway.Api/TheSouthlLibrary.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/ApiGateway/TheSouthlLibrary.Gateway.Api"
RUN dotnet build "TheSouthlLibrary.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheSouthlLibrary.Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheSouthlLibrary.Gateway.Api.dll"]