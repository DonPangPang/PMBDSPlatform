#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/PMBDS.JT808.Gateway/PMBDS.JT808.Gateway.csproj", "src/PMBDS.JT808.Gateway/"]
COPY ["src/PMBDS.PubSub.Abstractions/PMBDS.PubSub.Abstractions.csproj", "src/PMBDS.PubSub.Abstractions/"]
RUN dotnet restore "src/PMBDS.JT808.Gateway/PMBDS.JT808.Gateway.csproj"
COPY . .
WORKDIR "/src/src/PMBDS.JT808.Gateway"
RUN dotnet build "PMBDS.JT808.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PMBDS.JT808.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMBDS.JT808.Gateway.dll"]