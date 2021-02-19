#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Vulnerable.Api.Soap.Net5/Vulnerable.Api.Soap.Net5.csproj", "Vulnerable.Api.Soap.Net5/"]
COPY ["Vulnerable.Infrastructure.Data.Net5/Vulnerable.Infrastructure.Data.Net5.csproj", "Vulnerable.Infrastructure.Data.Net5/"]
COPY ["Vulnerable.Net5.Application/Vulnerable.Net5.Application.csproj", "Vulnerable.Net5.Application/"]
COPY ["Vulnerable.Application/Vulnerable.Application.csproj", "Vulnerable.Application/"]
COPY ["Vulnerable.Domain/Vulnerable.Domain.csproj", "Vulnerable.Domain/"]

RUN dotnet restore "Vulnerable.Api.Soap.Net5/Vulnerable.Api.Soap.Net5.csproj"
COPY . .
WORKDIR "/src/Vulnerable.Api.Soap.Net5.Api"
RUN dotnet build "Vulnerable.Api.Soap.Net5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vulnerable.Api.Soap.Net5.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vulnerable.Api.Soap.Net5.dll"]