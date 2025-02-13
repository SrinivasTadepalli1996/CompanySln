# Use .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Use .NET runtime for final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy built application
COPY --from=build /app/out ./

# Expose port 5168 to match local setup
EXPOSE 5168

# Ensure the app runs on this port
ENV ASPNETCORE_URLS=http://+:5168

# Start the application
ENTRYPOINT ["dotnet", "CompanyApi.dll"]

