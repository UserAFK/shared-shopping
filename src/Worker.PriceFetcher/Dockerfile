FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Worker.PriceFetcher.csproj", "Worker.PriceFetcher/"]
WORKDIR /src/Worker.PriceFetcher
RUN dotnet restore "Worker.PriceFetcher.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Worker.PriceFetcher.dll"]
