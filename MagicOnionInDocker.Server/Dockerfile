FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MagicOnionInDocker.Server/MagicOnionInDocker.Server.csproj MagicOnionInDocker.Server/
COPY MagicOnionInDocker.ServiceDefinition/MagicOnionInDocker.ServiceDefinition.csproj MagicOnionInDocker.ServiceDefinition/
RUN dotnet restore MagicOnionInDocker.Server/MagicOnionInDocker.Server.csproj
COPY . .
WORKDIR /src/MagicOnionInDocker.Server
RUN dotnet build MagicOnionInDocker.Server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MagicOnionInDocker.Server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MagicOnionInDocker.Server.dll"]
