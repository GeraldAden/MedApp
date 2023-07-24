#!/bin/bash

export VAULT_ADDR=http://localhost:8200/

vault login root

vault kv put secret/medapp db_username=admin db_password=pgadmin