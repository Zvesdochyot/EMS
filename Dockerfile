FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source
COPY *.sln .
COPY src/ src/

WORKDIR /source/src/EMS
RUN dotnet publish --configuration Release --output /build

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /build .
ENTRYPOINT ["dotnet", "EMS.dll", "--environment=Production"]
