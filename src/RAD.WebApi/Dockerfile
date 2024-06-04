#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RAD.WebApi/RAD.WebApi.csproj", "src/RAD.WebApi/"]
COPY ["src/RAD.DAL/RAD.DAL.csproj", "src/RAD.DAL/"]
COPY ["src/RAD.Domain/RAD.Domain.csproj", "src/RAD.Domain/"]
COPY ["src/RAD.DTOs/RAD.DTOs.csproj", "src/RAD.DTOs/"]
COPY ["src/RAD.Services/RAD.Services.csproj", "src/RAD.Services/"]
RUN dotnet restore "./src/RAD.WebApi/RAD.WebApi.csproj"
COPY . .
WORKDIR "/src/src/RAD.WebApi"
RUN dotnet build "./RAD.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RAD.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RAD.WebApi.dll"]