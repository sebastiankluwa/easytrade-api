FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build

EXPOSE 80
EXPOSE 443
WORKDIR /src
COPY ["easytrade/", "."]

WORKDIR "/src/Easytrade.Api"

RUN dotnet publish "Easytrade.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS final

RUN apt-get update \
    && apt-get dist-upgrade -y \
    && apt-get install -y --no-install-recommends curl

# Retrieve Datadog Dotnet APM
ARG DD_DOTNET_APM_VERSION="2.15.0"

RUN curl --silent -o datadog-dotnet-apm.deb \
  -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v${DD_DOTNET_APM_VERSION}/datadog-dotnet-apm_${DD_DOTNET_APM_VERSION}_amd64.deb \
  && dpkg -i ./datadog-dotnet-apm.deb \
  && rm ./datadog-dotnet-apm.deb \
  && mkdir -p /var/log/datadog/dotnet && chmod a+rwx /var/log/datadog/dotnet

# Set environment variables
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json
ENV DD_DOTNET_TRACER_HOME=/opt/datadog
ENV DD_RUNTIME_METRICS_ENABLED=true

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Easytrade.Api.dll"]
