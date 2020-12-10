#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /mkcert
RUN apt update -y && \
    apt-get update -y && \
    apt install libnss3-tools wget curl openssl -y && \    
    #wget https://github.com/FiloSottile/mkcert/archive/v1.4.3.tar.gz && \
    wget https://github.com/FiloSottile/mkcert/releases/download/v1.4.3/mkcert-v1.4.3-linux-arm && \
    #tar xzvf ./v1.4.3.tar.gz && \
    #cd mkcert-1.4.3/ && \
    #go build && \
    #mkdir /mkcert && \
    #cp bin/mkcert /mkcert
    mv mkcert-v1.4.3-linux-arm mkcert 
COPY . /usr/bin
WORKDIR /certificates
RUN chmod 777 mkcert && \
    #mkcert --install && \
    mkcert -key-file key.pem -cert-file cert.pem localhost && \
    openssl pkcs12 -export -out cert.pfx -inkey key.pem -in cert.pem -passout pass:RafsanSSL2020
WORKDIR /src
COPY ["Applications/Web/SPA/Server/RH.Apps.Web.SPA.Lite.csproj", "Applications/Web/SPA/Server/"]
RUN dotnet restore "Applications/Web/SPA/Server/RH.Apps.Web.SPA.Lite.csproj"
COPY . .
WORKDIR "/src/Applications/Web/SPA/Server"
COPY /certificates/cert.pfx /root/.aspnet/https/RH.Apps.Web.SPA.Lite.pfx
RUN dotnet build "RH.Apps.Web.SPA.Lite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RH.Apps.Web.SPA.Lite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /certificates/cert.pfx /root/.aspnet/https/RH.Apps.Web.SPA.Lite.pfx
ENTRYPOINT ["dotnet", "RH.Apps.Web.SPA.Lite.dll"]