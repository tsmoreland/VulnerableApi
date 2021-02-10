#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Vulnerable.Net5.Soap.Api/Vulnerable.Net5.Soap.Api.csproj", "Vulnerable.Net5.Soap.Api/"]
COPY ["Vulnerable.Net5.Data/Vulnerable.Net5.Data.csproj", "Vulnerable.Net5.Data/"]
RUN dotnet restore "Vulnerable.Net5.Soap.Api/Vulnerable.Net5.Soap.Api.csproj"
COPY . .
WORKDIR "/src/Vulnerable.Net5.Soap.Api.Api"
RUN dotnet build "Vulnerable.Net5.Soap.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vulnerable.Net5.Soap.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vulnerable.Net5.Soap.Api.dll"]