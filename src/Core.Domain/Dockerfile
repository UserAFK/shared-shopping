FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Core.Domain.csproj", "Core.Domain/"]
WORKDIR /src/Core.Domain
RUN dotnet restore "Core.Domain.csproj"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Core.Domain.dll"]
