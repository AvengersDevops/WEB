FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY AvengersWeb.csproj .
RUN dotnet restore AvengersWeb.csproj
COPY . .
RUN dotnet build AvengersWeb.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish AvengersWeb.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
