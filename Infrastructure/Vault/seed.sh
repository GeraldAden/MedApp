#!/bin/bash

export VAULT_ADDR=http://localhost:8200/

vault login root

# vault kv put secret/medapp db_username=admin db_password=pgadmin

vault kv put secret/medapp ConnectionString="Host=localhost;Port=5432;Database=meddb;Username=medapp;Password=apppw;"