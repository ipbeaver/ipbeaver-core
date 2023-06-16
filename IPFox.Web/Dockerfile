#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RealIP.Web/RealIP.Web.csproj", "RealIP.Web/"]
RUN dotnet restore "RealIP.Web/RealIP.Web.csproj"
COPY . .
WORKDIR "/src/RealIP.Web"
RUN dotnet build "RealIP.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealIP.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ip.db .
ENTRYPOINT ["dotnet", "RealIP.Web.dll"]