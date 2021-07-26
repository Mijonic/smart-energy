#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SmartEnergy.MicroserviceAPI/SmartEnergy.MicroserviceAPI.csproj", "SmartEnergy.MicroserviceAPI/"]
COPY ["SmartEnergy.Contract/SmartEnergy.Contract.csproj", "SmartEnergy.Contract/"]
RUN dotnet restore "SmartEnergy.MicroserviceAPI/SmartEnergy.MicroserviceAPI.csproj"
COPY . .
WORKDIR "/src/SmartEnergy.MicroserviceAPI"
RUN dotnet build "SmartEnergy.MicroserviceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartEnergy.MicroserviceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartEnergy.MicroserviceAPI.dll"]
