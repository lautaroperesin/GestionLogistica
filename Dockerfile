# Imagen base para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo el .csproj para aprovechar el cache en builds
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del código fuente y publica
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Imagen final solo con lo necesario para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configura el puerto donde escucha la app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Copia los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# Comando de inicio
ENTRYPOINT ["dotnet", "GestionLogisticaBackend.dll"]
