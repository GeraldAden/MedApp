CREATE SCHEMA IF NOT EXISTS records;
SET SEARCH_PATH TO records;

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password_salt VARCHAR(50) NOT NULL,
    password_hash VARCHAR(256) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NULL DEFAULT NULL
);