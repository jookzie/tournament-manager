CREATE DATABASE IF NOT EXISTS your_database_name;
USE your_database_name;

CREATE TABLE users (
    id BINARY(16) NOT NULL,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    accountType ENUM('User', 'Admin') NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE tournaments (
    id BINARY(16) NOT NULL,
    sportType ENUM('Badminton') NOT NULL,
    tournamentSystem VARCHAR(255) NOT NULL,
    location VARCHAR(255) NOT NULL,
    description TEXT,
    startDate DATETIME NOT NULL,
    endDate DATETIME NOT NULL,
    maxCapacity INT NOT NULL,
    minCapacity INT NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE users_tournaments (
    user_id BINARY(16) NOT NULL,
    tournament_id BINARY(16) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (tournament_id) REFERENCES tournaments(id)
);

CREATE TABLE schedule (
    tournament_id BINARY(16) NOT NULL,
    round_num INT NOT NULL,
    match_num INT NOT NULL,
    game_num INT NOT NULL,
    player1_id BINARY(16) NOT NULL,
    player2_id BINARY(16) NOT NULL,
    score1 INT NOT NULL,
    score2 INT NOT NULL,
    skipper_id BINARY(16) NOT NULL,
    FOREIGN KEY (tournament_id) REFERENCES tournaments(id),
    FOREIGN KEY (player1_id) REFERENCES users(id),
    FOREIGN KEY (player2_id) REFERENCES users(id),
    FOREIGN KEY (skipper_id) REFERENCES users(id)
);