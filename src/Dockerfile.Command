# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Core.Domain/.", "Core.Domain/"]
COPY ["Infrastructure.Data/.", "Infrastructure.Data/"]
COPY ["Service.Command/.", "Service.Command/"]

WORKDIR /src/Service.Command
RUN dotnet restore

RUN dotnet publish -c Release -o /app/publish

# Stage 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Service.Command.dll"]