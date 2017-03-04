
FROM microsoft/dotnet:1.0-runtime
LABEL Name=netcore-rc4-nancy-api Version=0.0.2
WORKDIR /app
EXPOSE 5000
COPY out .
ENTRYPOINT dotnet myapi.dll
