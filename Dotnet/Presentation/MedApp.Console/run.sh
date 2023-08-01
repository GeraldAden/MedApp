#!/bin/bash

export DOTNET_ENVIRONMENT=Development
export VAULT_ADDR=http://localhost:8200/

docker compose -f ../../../Infrastructure/infrastructure-compose.yml up -d

echo "Vault login"
vault login

../../../Infrastructure/Vault/seed.sh

export ConnectionStrings__MedDb=$(vault kv get -field=ConnectionString secret/medapp)

echo "Connection string: $ConnectionStrings__MedDb"

dotnet run

docker compose -f ../../../Infrastructure/infrastructure-compose.yml down
