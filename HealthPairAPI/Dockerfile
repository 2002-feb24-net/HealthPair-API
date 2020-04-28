FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["HealthPairAPI/HealthPairAPI.csproj", "HealthPairAPI/"]
RUN dotnet restore "HealthPairAPI/HealthPairAPI.csproj"
COPY . .
WORKDIR "/src/HealthPairAPI"
RUN dotnet build "HealthPairAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HealthPairAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthPairAPI.dll"]
