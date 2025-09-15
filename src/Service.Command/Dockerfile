FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Service.Command.csproj", "Service.Command/"]
WORKDIR /src/Service.Command
RUN dotnet restore "Service.Command.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Service.Command.dll"]
