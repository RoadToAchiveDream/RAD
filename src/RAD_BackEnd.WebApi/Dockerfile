#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RAD_BackEnd.WebApi/RAD_BackEnd.WebApi.csproj", "src/RAD_BackEnd.WebApi/"]
COPY ["src/RAD_BackEnd.DataAccess/RAD_BackEnd.DataAccess.csproj", "src/RAD_BackEnd.DataAccess/"]
COPY ["src/RAD_BackEnd.Domain/RAD_BackEnd.Domain.csproj", "src/RAD_BackEnd.Domain/"]
COPY ["src/RAD_BackEnd.Services/RAD_BackEnd.Services.csproj", "src/RAD_BackEnd.Services/"]
COPY ["src/RAD_BackEnd.DTOs/RAD_BackEnd.DTOs.csproj", "src/RAD_BackEnd.DTOs/"]
RUN dotnet restore "./src/RAD_BackEnd.WebApi/RAD_BackEnd.WebApi.csproj"
COPY . .
WORKDIR "/src/src/RAD_BackEnd.WebApi"
RUN dotnet build "./RAD_BackEnd.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RAD_BackEnd.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RAD_BackEnd.WebApi.dll"]