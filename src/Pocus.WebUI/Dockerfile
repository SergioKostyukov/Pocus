FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y git

COPY ["src/Pocus.WebUI/Pocus.WebUI.csproj", "src/Pocus.WebUI/"]
COPY ["src/Pocus.Application/Pocus.Application.csproj", "src/Pocus.Application/"]
COPY ["src/Pocus.Core/Pocus.Core.csproj", "src/Pocus.Core/"]
COPY ["src/Pocus.Infrastructure.Data/Pocus.Infrastructure.Data.csproj", "src/Pocus.Infrastructure.Data/"]
COPY ["src/Pocus.Infrastructure/Pocus.Infrastructure.csproj", "src/Pocus.Infrastructure/"]

RUN dotnet restore "src/Pocus.WebUI/Pocus.WebUI.csproj"

COPY . .
WORKDIR "/src/src/Pocus.WebUI"
RUN dotnet build "Pocus.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/src/Pocus.WebUI
RUN dotnet publish "Pocus.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pocus.WebUI.dll"]
