FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Api.Gateway.csproj", "Api.Gateway/"]
WORKDIR /src/Api.Gateway
RUN dotnet restore "Api.Gateway.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Api.Gateway.dll"]
