FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /webapi_app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["webapi-docker.csproj", "./"]
RUN dotnet restore "webapi-docker.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "webapi-docker.csproj" -c Release -o /webapi_app/build

FROM build AS publish
RUN dotnet publish "webapi-docker.csproj" -c Release -o /webapi_app/publish \
   -r alpine-x64 \
   --self-contained true \
   -p:PublishTrimmed=true \
   -p:PublishSingleFile=true

FROM base AS final
WORKDIR /webapi_app
COPY --from=publish /webapi_app/publish .
ENTRYPOINT [ "dotnet", "webapi-docker.dll" ]