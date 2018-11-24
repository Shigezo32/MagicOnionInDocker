FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MagicOnionInDocker.HttpGateway/MagicOnionInDocker.HttpGateway.csproj MagicOnionInDocker.HttpGateway/
COPY MagicOnionInDocker.Server/MagicOnionInDocker.Server.csproj MagicOnionInDocker.Server/
COPY MagicOnionInDocker.ServiceDefinition/MagicOnionInDocker.ServiceDefinition.csproj MagicOnionInDocker.ServiceDefinition/
RUN dotnet restore MagicOnionInDocker.HttpGateway/MagicOnionInDocker.HttpGateway.csproj
COPY . .
WORKDIR /src/MagicOnionInDocker.HttpGateway
RUN dotnet build MagicOnionInDocker.HttpGateway.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MagicOnionInDocker.HttpGateway.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MagicOnionInDocker.HttpGateway.dll"]
