# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /src
# COPY ["HealthPairAPI.csproj", "./"]
# RUN dotnet restore "./HealthPairAPI.csproj"
# COPY . .
# RUN dotnet build "HealthPairAPI.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "HealthPairAPI.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "HealthPairAPI.dll"]


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./
COPY HealthPairAPI/*.csproj HealthPairAPI/
COPY HealthPairDomain/*.csproj HealthPairDomain/
COPY HealthPairDataAccess/*.csproj HealthPairDataAccess/
COPY HealthPairTests/*.csproj HealthPairTests/
RUN dotnet restore

COPY . ./
RUN dotnet publish HealthPairAPI -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HealthPairAPI.dll"]
