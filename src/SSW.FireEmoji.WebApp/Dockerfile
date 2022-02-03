#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["model/gitmo.mlnet", "model/gitmo.mlnet"]
COPY ["src/SSW.FireEmoji.WebApp/SSW.FireEmoji.WebApp.csproj", "src/SSW.FireEmoji.WebApp/"]
COPY ["src/SSW.FireEmoji.Core/SSW.FireEmoji.Core.csproj", "src/SSW.FireEmoji.Core/"]
RUN dotnet restore "src/SSW.FireEmoji.WebApp/SSW.FireEmoji.WebApp.csproj"
COPY . .
WORKDIR "/src/src/SSW.FireEmoji.WebApp"
RUN dotnet build "SSW.FireEmoji.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSW.FireEmoji.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSW.FireEmoji.WebApp.dll"]