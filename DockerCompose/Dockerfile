# Establecer la imagen base para la aplicación en tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Establecer la imagen base para el SDK y la construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo csproj y restaurar las dependencias
COPY ["DockerCompose/DockerCompose.csproj", "DockerCompose/"]
RUN dotnet restore "DockerCompose/DockerCompose.csproj"



# Copiar el resto del código y construir el proyecto
COPY . .
WORKDIR "/src/DockerCompose"
RUN dotnet build "DockerCompose.csproj" -c Release -o /app/build

# Instalar la herramienta dotnet-ef
RUN dotnet tool install --global dotnet-ef

# Agregar la herramienta dotnet-ef al PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Lanzamos las migraciones de entidades y actualizamos la base de datos

RUN dotnet ef migrations add InitMigration
RUN dotnet ef database update

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "DockerCompose.csproj" -c Release -o /app/publish

# Establecer la imagen final y copiar los archivos publicados
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "DockerCompose.dll"]
