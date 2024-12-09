# Base image for production (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image (Linux)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["waytodine_sem9/waytodine_sem9.csproj", "waytodine_sem9/"]
RUN dotnet restore "./waytodine_sem9/waytodine_sem9.csproj"
COPY . .
WORKDIR "/src/waytodine_sem9"
RUN dotnet build "./waytodine_sem9.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./waytodine_sem9.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "waytodine_sem9.dll"]