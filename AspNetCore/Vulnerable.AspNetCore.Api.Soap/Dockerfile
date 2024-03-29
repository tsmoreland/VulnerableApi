#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Vulnerable.AspNetCore.Api.Soap/Vulnerable.Net5.Api.Soap.csproj", "Vulnerable.AspNetCore.Api.Soap/"]
COPY ["Vulnerable.Infrastructure.Data.AspNetCore/Vulnerable.Infrastructure.Data.AspNetCore.csproj", "Vulnerable.Infrastructure.Data.Net5/"]
COPY ["Vulnerable.AspNetCore.Application/Vulnerable.Net5.Application.csproj", "Vulnerable.Net5.Application/"]
COPY ["Vulnerable.Application/Vulnerable.Application.csproj", "Vulnerable.Application/"]
COPY ["Vulnerable.Domain/Vulnerable.Domain.csproj", "Vulnerable.Domain/"]

RUN dotnet restore "Vulnerable.Net5.Api.Soap/Vulnerable.Net5.Api.Soap.csproj"
COPY . .
WORKDIR "/src/Vulnerable.AspNetCore.Api.Soap.Api"
RUN dotnet build "Vulnerable.AspNetCore.Api.Soap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vulnerable.AspNetCore.Api.Soap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vulnerable.Net5.Api.Soap.dll"]
