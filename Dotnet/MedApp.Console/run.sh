#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Development

docker compose -f ../../Infrastructure/infrastructure-compose.yml up -d

vault login

../../Infrastructure/Vault/seed.sh

export ConnectionStrings__MedDb=$(vault kv get -field=ConnectionString secret/medapp)

dotnet run

docker compose -f ../../Infrastructure/infrastucture-compose.yml down