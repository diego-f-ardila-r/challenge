FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Metafar.Challenge.Entity/Metafar.Challenge.Entity.csproj", "Metafar.Challenge.Entity/"]
COPY ["Metafar.Challenge.Model/Metafar.Challenge.Model.csproj", "Metafar.Challenge.Model/"]
COPY ["Metafar.Challenge.Dto/Metafar.Challenge.Dto.csproj", "Metafar.Challenge.Dto/"]
COPY ["Metafar.Challenge.Infrastructure/Metafar.Challenge.Infrastructure.csproj", "Metafar.Challenge.Infrastructure/"]
COPY ["Metafar.Challenge.Repository/Metafar.Challenge.Repository.csproj", "Metafar.Challenge.Repository/"]
COPY ["Metafar.Challenge.UseCase/Metafar.Challenge.UseCase.csproj", "Metafar.Challenge.UseCase/"]
COPY ["Metafar.Challenge.WebApi/Metafar.Challenge.WebApi.csproj", "Metafar.Challenge.WebApi/"]


RUN dotnet restore "Metafar.Challenge.WebApi/Metafar.Challenge.WebApi.csproj"
COPY . .
WORKDIR "/src/Metafar.Challenge.WebApi"
RUN dotnet build "Metafar.Challenge.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Metafar.Challenge.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metafar.Challenge.WebApi.dll"]
