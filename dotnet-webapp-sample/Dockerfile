FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-alpine AS restore
WORKDIR /src
COPY /src .
RUN dotnet restore

FROM restore AS publish
WORKDIR /src/WebApplicationTemplate
RUN dotnet publish "WebApplicationTemplate.csproj" -c Release -o /app

FROM restore AS testrunner
RUN dotnet build
WORKDIR /src/UnitTests
ENTRYPOINT ["dotnet", "test", "--logger:trx;LogFileName=WebApplicationTemplate.TestResults.xml"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApplicationTemplate.dll"]