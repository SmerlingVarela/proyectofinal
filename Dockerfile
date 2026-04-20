FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AsistenciaApp/AsistenciaApp.csproj"
RUN dotnet build "AsistenciaApp/AsistenciaApp.csproj" -c Release -o /app/build
RUN dotnet publish "AsistenciaApp/AsistenciaApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "AsistenciaApp.dll"]