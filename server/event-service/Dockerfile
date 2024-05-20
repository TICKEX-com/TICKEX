#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["event-service.csproj", "./"]
RUN dotnet restore "./event-service.csproj"
COPY . .
RUN dotnet build "event-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "event-service.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "event-service.dll","--urls", "http://*:8080"]