# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Api.BootCamp.csproj", "./"]
RUN dotnet restore "Api.BootCamp.csproj"

COPY . .
RUN dotnet publish "Api.BootCamp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

RUN adduser --disabled-password appuser
USER appuser

EXPOSE 8080
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.BootCamp.dll"]
