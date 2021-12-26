# Stage 1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source
COPY *.sln .
COPY src/ src/

WORKDIR /source/src/EMS
RUN dotnet publish --configuration Release --output /build

# Stage 2
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /build .
EXPOSE 80
ENTRYPOINT ["dotnet", "EMS.dll", "--environment=Production"]
