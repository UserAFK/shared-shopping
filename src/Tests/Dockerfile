FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SharedShopping.Tests.csproj", "Tests/"]
WORKDIR /src/Tests
RUN dotnet restore "SharedShopping.Tests.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SharedShopping.Tests.dll"]
