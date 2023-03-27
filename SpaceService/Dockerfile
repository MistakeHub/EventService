#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SpaceService/SpaceService.csproj", "SpaceService/"]
COPY ["EventService/EventService.csproj", "EventService/"]
COPY ["SC.Internship.Common/SC.Internship.Common/SC.Internship.Common.csproj", "SC.Internship.Common/SC.Internship.Common/"]
RUN dotnet restore "SpaceService/SpaceService.csproj"
COPY . .
WORKDIR "/src/SpaceService"
RUN dotnet build "SpaceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpaceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpaceService.dll"]