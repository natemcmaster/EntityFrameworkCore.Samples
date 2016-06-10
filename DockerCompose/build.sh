#!/usr/bin/env bash
set -e
rm -rf DockerWebApp/bin/
dotnet publish -c Release DockerWebApp/
docker-compose up --build