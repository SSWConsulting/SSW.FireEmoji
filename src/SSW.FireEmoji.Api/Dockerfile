#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SSW.FireEmoji.Api/SSW.FireEmoji.Api.csproj", "src/SSW.FireEmoji.Api/"]
COPY ["src/SSW.FireEmoji.Core/SSW.FireEmoji.Core.csproj", "src/SSW.FireEmoji.Core/"]
RUN dotnet restore "src/SSW.FireEmoji.Api/SSW.FireEmoji.Api.csproj"
COPY . .
WORKDIR "/src/src/SSW.FireEmoji.Api"
RUN dotnet build "SSW.FireEmoji.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSW.FireEmoji.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSW.FireEmoji.Api.dll"]