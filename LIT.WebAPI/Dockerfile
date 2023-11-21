#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LIT.WebAPI/LIT.WebAPI.csproj", "LIT.WebAPI/"]
COPY ["LIT.Application/LIT.Application.csproj", "LIT.Application/"]
COPY ["LIT.Domain/LIT.Domain.csproj", "LIT.Domain/"]
COPY ["LIT.Infra/LIT.Infra.csproj", "LIT.Infra/"]
RUN dotnet restore "./LIT.WebAPI/./LIT.WebAPI.csproj"
COPY . .
WORKDIR "/src/LIT.WebAPI"
RUN dotnet build "./LIT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LIT.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LIT.WebAPI.dll"]