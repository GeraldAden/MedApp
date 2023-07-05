CREATE SCHEMA IF NOT EXISTS records;
SET SEARCH_PATH TO records;

CREATE TABLE patients (
  id SERIAL PRIMARY KEY,
  first_name VARCHAR(255) NOT NULL,
  last_name VARCHAR(255) NOT NULL,
  date_of_birth DATE NOT NULL,
  email VARCHAR(50) NOT NULL,
  is_smoker BOOLEAN NOT NULL,
  has_cancer BOOLEAN NOT NULL,
  has_diabetes BOOLEAN NOT NULL,
  created_at TIMESTAMP NOT NULL,
  updated_at TIMESTAMP
);