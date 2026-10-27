FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["student-results-csharp.csproj", "./"]
RUN dotnet restore "student-results-csharp.csproj"
COPY . .
RUN dotnet publish "student-results-csharp.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
RUN mkdir /data && chown app:app /data
USER app
WORKDIR /data
ENTRYPOINT ["dotnet", "/app/student-results-csharp.dll"]