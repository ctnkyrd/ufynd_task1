# Task 1 : Web Extractionn

## Environment
Framework: .Net 5

### Input Html Stream
Used http-server for serving html content provided with the assignment. Below command runs the http server to prepare resources for webscrapping.

```
python3 -m http.server 8000
```

### Projects 
* task1.app: .Net Console Application
* task1.test: .Net NUnit Test Project

## Build & Run
```
dotnet build && dotnet test

dotnet run -p task1.app/task1.app.csproj
```
