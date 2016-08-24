#!/usr/bin/env bash

set -e

dbserver="${DB_HOSTNAME}"
if [ -z "$dbserver" ]; then 
    dbserver="dbserver"
fi

dbport="${DB_PORT}"
if [ -z "$dbport" ]; then
    dbport=5432
fi

until nc -z $dbserver $dbport; do
    echo "$(date) - waiting for ${dbserver}:${dbport}..."
    sleep 1
done

# How to apply migrations before the application launches
echo "Applying migrations"
dotnet exec \
    --runtimeconfig DockerWebApp.runtimeconfig.json \
    --depsfile DockerWebApp.deps.json \
    Microsoft.EntityFrameworkCore.Design.dll \
    --assembly DockerWebApp.dll \
    --verbose \
    database update

# Start web app
echo "Starting web app"

# TODO use a crash-resilient runner
dotnet DockerWebApp.dll