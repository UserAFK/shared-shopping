FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Infrastructure.Data.csproj", "Infrastructure.Data/"]
WORKDIR /src/Infrastructure.Data
RUN dotnet restore "Infrastructure.Data.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Infrastructure.Data.dll"]
