# Stage 1: Build the .NET project
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["waytodine_sem9/waytodine_sem9.csproj", "waytodine_sem9/"]
RUN dotnet restore "./waytodine_sem9/waytodine_sem9.csproj"

# Copy the remaining project files and build the project
COPY . .
WORKDIR "/src/waytodine_sem9"
RUN dotnet build "./waytodine_sem9.csproj" -c Release -o /app/build

# Stage 2: Publish the .NET project
FROM build AS publish
RUN dotnet publish "./waytodine_sem9.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Run the .NET application in the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy the published application from the previous stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "waytodine_sem9.dll"]