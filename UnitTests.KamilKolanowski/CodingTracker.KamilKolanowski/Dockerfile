FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore "CodingTracker.KamilKolanowski.csproj"
RUN dotnet publish "CodingTracker.KamilKolanowski.csproj" -c Release -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "CodingTracker.KamilKolanowski.dll"]
