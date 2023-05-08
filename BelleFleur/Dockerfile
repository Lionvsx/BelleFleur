FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BelleFleur/BelleFleur.csproj", "BelleFleur/"]
RUN dotnet restore "BelleFleur/BelleFleur.csproj"
COPY . .
WORKDIR "/src/BelleFleur"
RUN dotnet build "BelleFleur.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BelleFleur.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BelleFleur.dll"]
