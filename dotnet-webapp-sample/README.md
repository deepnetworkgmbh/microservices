# DOTNET SAMPLE

Set the current context to `dotnet-webapp-sample`

## Build

- From *Visual Studio* press Ctrl+Shift+B
- From *terminal* `dotnet build ./src/WebApplicationTemplate/WebApplicationTemplate.csproj`
- From *terminal* `docker build --no-cache --force-rm --target final -t webapptemplate .`

## Test localy

- From *terminal* `dotnet test ./src/UnitTests/UnitTests.csproj`
- Using *Docker*
  - build an image `docker build --no-cache --force-rm --target testrunner -t webapptemplate:tests .`
  - run tests `docker run --rm --mount type=bind,source=${ABSOLUTE_PATH}/testresults,target=/src/UnitTests/TestResults/ webapptemplate:tests`

## Run localy

- From *Visual Studio* press F5
- From *terminal* `dotnet run -p ./src/WebApplicationTemplate/WebApplicationTemplate.csproj`
- From *terminal* `docker run -p 8888:80 webapptemplate`