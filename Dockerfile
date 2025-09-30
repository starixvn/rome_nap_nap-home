
FROM hub.nexdev.net/base/dotnet/core/sdk:8.0-alpine-25h2 AS builder
WORKDIR /src

COPY ./Upgrade/Fanex.NAP.Home.Sites/Fanex.NAP.Home.Sites.csproj ./Fanex.NAP.Home.Sites/

WORKDIR /src/Fanex.NAP.Home.Sites
RUN dotnet nuget add source https://nuget.nexdev.net/api/v2
RUN dotnet restore -v n

COPY ./Upgrade/Fanex.NAP.Home.Sites .

RUN dotnet publish -c Release -o /app

FROM hub.nexdev.net/base/dotnet/core/aspnet:8.0-alpine-25h2 AS base

# Don�t run as root
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app && chown -R dotnetuser /tmp
USER dotnetuser
#

WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
COPY --from=builder /app .
ENV TZ="America/La_Paz"

ENTRYPOINT ["dotnet", "Fanex.NAP.Home.Sites.dll"]