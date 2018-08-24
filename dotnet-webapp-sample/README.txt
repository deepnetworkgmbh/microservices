Set the current context to `dotnet-webapp-sample`

## Build

- From *Visual Studio* press Ctrl+Shift+B
- From *terminal* `dotnet build`
- From *terminal* `docker build -f WebApplicationTemplate\dockerfile -t webapptemplate .`

## Run

- From *Visual Studio* press F5
- From *terminal* `dotnet run`
- From *terminal* `docker run -p 8888:80 webapptemplate`

## Test

- From *terminal* `dotnet test`