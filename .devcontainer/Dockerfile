# [Choice] .NET Core version: 5.0, 3.1, 2.1
ARG VARIANT=5.0
FROM mcr.microsoft.com/vscode/devcontainers/dotnetcore:${VARIANT}

# [Option] Install Node.js
ARG INSTALL_NODE="true"
ARG NODE_VERSION="lts/*"
RUN if [ "${INSTALL_NODE}" = "true" ]; then su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install ${NODE_VERSION} 2>&1"; fi

# [Option] Install Azure CLI
ARG INSTALL_AZURE_CLI="true"
COPY library-scripts/azcli-debian.sh /tmp/library-scripts/
RUN if [ "$INSTALL_AZURE_CLI" = "true" ]; then bash /tmp/library-scripts/azcli-debian.sh; fi \
    && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

# Install SQL Tools: SQLPackage and sqlcmd
COPY mssql/installSQLtools.sh installSQLtools.sh
RUN bash ./installSQLtools.sh -- -y \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

# Update args in docker-compose.yaml to set the UID/GID of the "vscode" user.
ARG USER_UID=1000
ARG USER_GID=$USER_UID
RUN if [ "$USER_GID" != "1000" ] || [ "$USER_UID" != "1000" ]; then \
        groupmod --gid $USER_GID vscode \
        && usermod --uid $USER_UID --gid $USER_GID vscode \
        && chown -R $USER_UID:$USER_GID /home/vscode \
        && chown -R $USER_UID:root /usr/local/share/nvm /usr/local/share/npm-global; \
    fi

#WORKDIR /mkcert
#RUN apt-get update -y && \
    #apt-get update -y && \
	#apt-get upgrade -y && \
    #apt-get install libnss3-tools wget curl openssl -y && \
    #wget https://github.com/FiloSottile/mkcert/releases/download/v1.4.2/mkcert-v1.4.2-linux-arm && \
    #mv mkcert-v1.4.2-linux-arm /usr/bin/mkcert && \
	#apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts && \
	#apt-get autoremove -y

#RUN chmod 777 /usr/bin/mkcert && \
	#mkcert -key-file /key.pem -cert-file /cert.pem localhost && \
	#openssl pkcs12 -export -out /aspnetapp.pfx -inkey /key.pem -in /cert.pem -certfile /root/.local/share/mkcert/rootCA.pem -passout pass:"P@ssw0rd" 
#WORKDIR /home/vscode/.aspnet/https/ 
#RUN mv /aspnetapp.pfx .

WORKDIR /workspace
# [Optional] Uncomment this section to install additional OS packages.
#RUN apt-get update && export DEBIAN_FRONTEND=noninteractive && \
#    apt-get -y install --no-install-recommends openssl

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1