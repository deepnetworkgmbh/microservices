FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /src
COPY ["WebApplicationTemplate/WebApplicationTemplate.csproj", "WebApplicationTemplate/"]
RUN dotnet restore "WebApplicationTemplate/WebApplicationTemplate.csproj"
COPY . .
WORKDIR "/src/WebApplicationTemplate"
RUN dotnet build "WebApplicationTemplate.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApplicationTemplate.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApplicationTemplate.dll"]