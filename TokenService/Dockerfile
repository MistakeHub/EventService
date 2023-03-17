FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY TokenService/TokenService.csproj TokenService/TokenService.csproj
RUN dotnet restore TokenService.csproj
COPY . .
WORKDIR /src
RUN dotnet build TokenService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TokenService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TokenService.csproj.dll"]