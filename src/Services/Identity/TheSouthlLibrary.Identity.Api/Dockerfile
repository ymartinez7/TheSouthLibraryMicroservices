#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Identity/TheSouthlLibrary.Identity.Api/TheSouthlLibrary.Identity.Api.csproj", "Identity/TheSouthlLibrary.Identity.Api/"]
RUN dotnet restore "Identity/TheSouthlLibrary.Identity.Api/TheSouthlLibrary.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Identity/TheSouthlLibrary.Identity.Api"
RUN dotnet build "TheSouthlLibrary.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheSouthlLibrary.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheSouthlLibrary.Identity.Api.dll"]