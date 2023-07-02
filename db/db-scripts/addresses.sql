CREATE SCHEMA IF NOT EXISTS records;
SET SEARCH_PATH TO records;

CREATE TABLE addresses (
  street VARCHAR(255) NOT NULL,
  city VARCHAR(255) NOT NULL,
  state VARCHAR(255) NOT NULL,
  zip_code VARCHAR(255) NOT NULL,
  is_mailing_address BOOLEAN NOT NULL DEFAULT FALSE,
  address_id INTEGER NOT NULL REFERENCES addresses(id)
);