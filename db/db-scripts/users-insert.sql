SET SEARCH_PATH TO records;

INSERT INTO users (first_name, last_name, username, email, salt, hashed_password)
VALUES (
    'John',
    'Doe',
    'johndoe',
    'johndoe@example.com',
    'cZv0TStO5Tgj41leZg+vLCGg75AL/KlL+lV15GbQ098=',
    'QKfLAcZdfFc1vYRGyadc65vshqY8Feoh+V4BMu+bhZflJL+s6K++z/FiIEe+3b7/EoP1lNExvjrJfU+M5Knojw=='
);