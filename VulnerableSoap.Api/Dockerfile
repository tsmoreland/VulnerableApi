#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VulnerableSoap.Api/Moreland.VulnerableSoap.Api.csproj", "VulnerableSoap.Api/"]
COPY ["VulnerableSoap.Data/Moreland.VulnerableSoap.Data.csproj", "VulnerableSoap.Data/"]
RUN dotnet restore "VulnerableSoap.Api/Moreland.VulnerableSoap.Api.csproj"
COPY . .
WORKDIR "/src/VulnerableSoap.Api"
RUN dotnet build "Moreland.VulnerableSoap.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Moreland.VulnerableSoap.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Moreland.VulnerableSoap.Api.dll"]