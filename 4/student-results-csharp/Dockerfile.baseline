FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY . .
RUN dotnet build -c Release
CMD ["dotnet", "bin/Release/net9.0/student-results-csharp.dll"]