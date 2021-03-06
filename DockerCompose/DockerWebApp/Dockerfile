# TODO use official docker image
FROM buildpack-deps:jessie-scm

# Work around https://github.com/dotnet/cli/issues/1582 until Docker releases a
# fix (https://github.com/docker/docker/issues/20818). This workaround allows
# the container to be run with the default seccomp Docker settings by avoiding
# the restart_syscall made by LTTng which causes a failed assertion.
ENV LTTNG_UST_REGISTER_TIMEOUT 0

# Install .NET CLI dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        clang-3.5 \
        libc6 \
        libcurl3 \
        libgcc1 \
        libicu52 \
        liblttng-ust0 \
        libssl1.0.0 \
        libstdc++6 \
        libtinfo5 \
        libunwind8 \
        libuuid1 \
        zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Install .NET Core SDK
ENV DOTNET_CORE_SDK_VERSION 1.0.0-preview2-002996
RUN curl -SL https://dotnetcli.blob.core.windows.net/dotnet/preview/Binaries/$DOTNET_CORE_SDK_VERSION/dotnet-dev-debian-x64.$DOTNET_CORE_SDK_VERSION.tar.gz --output dotnet.tar.gz \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

## end create dotnet image

# Install netcat so that we can ping the database server until it
RUN apt-get update -qq \
    && apt-get install -y netcat \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /opt/app

ENV ASPNETCORE_ENVIRONMENT Production
EXPOSE 5000

COPY "./bin/Release/netcoreapp1.0/publish/" .

CMD chmod +x ./docker-start.sh
CMD bash ./docker-start.sh
