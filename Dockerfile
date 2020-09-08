FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-image
WORKDIR /app

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

EXPOSE 80

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Build da aplicação
COPY . ./
RUN dotnet publish --configuration ${BUILDCONFIG} -o out /p:Version=${VERSION}

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-image /app/out .
ENTRYPOINT ["dotnet", "LibraryApi.dll"]