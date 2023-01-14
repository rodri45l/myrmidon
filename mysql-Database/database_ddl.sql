-- Create the database and an administration user
CREATE DATABASE myrmidon;

USE myrmidon;

CREATE USER myrmidon_admin IDENTIFIED BY 'Icaro45z';

GRANT SELECT, UPDATE, INSERT ON myrmidon.* TO myrmidon_admin;
FLUSH PRIVILEGES;

-- Create tables
CREATE TABLE User
(
    user_id      CHAR(36)     NOT NULL UNIQUE,
    therapist_id CHAR(36) UNIQUE,
    name         VARCHAR(100) NOT NULL,
    surname      VARCHAR(100) NOT NULL,
    birth_date   DATETIME     NOT NULL,
    postal_code  VARCHAR(12)  NOT NULL,
    email        VARCHAR(320) NOT NULL UNIQUE,
    address      VARCHAR(320) NOT NULL,
    phone        VARCHAR(12)  NOT NULL,
    sex          BOOLEAN      NOT NULL,
    gender       VARCHAR(20),
    password     VARCHAR(255) NOT NULL, -- scrypt
    salt         VARCHAR(60)  NOT NULL,
    PRIMARY KEY (user_id)
);

ALTER TABLE User
    MODIFY password VARCHAR(255) COLLATE utf8mb4_bin;

CREATE TABLE appointments
(
    appointment_id INT AUTO_INCREMENT PRIMARY KEY,
    date           DATETIME NOT NULL,
    notes          VARCHAR(1000),
    patient        CHAR(36) NOT NULL,
    therapist      CHAR(36) NOT NULL,
    FOREIGN KEY (patient) REFERENCES User (user_id),
    FOREIGN KEY (therapist) REFERENCES User (therapist_id)
);

CREATE TABLE session_tokens
(
    token_id        VARCHAR(255) PRIMARY KEY,
    user_id         VARCHAR(255) NOT NULL,
    expiration_time TIMESTAMP    NOT NULL,
    ip_address      VARCHAR(45)  NOT NULL,
    FOREIGN KEY (user_id) REFERENCES User (user_id)
);

CREATE UNIQUE INDEX user_id_idx ON session_tokens (user_id);
CREATE INDEX token_id_idx ON session_tokens (token_id);


CREATE TABLE `Patient`
(
    patient         CHAR(36) NOT NULL,
    therapist       CHAR(36) UNIQUE,
    medical_history VARCHAR(1000),
    PRIMARY KEY (patient, `therapist`),
    CONSTRAINT `Constr_Patient_patient_fk`
        FOREIGN KEY `patient_fk` (`patient`) REFERENCES `User` (`user_id`)
            ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT `Constr_Patient_therapist_fk`
        FOREIGN KEY `therapist_fk` (`therapist`) REFERENCES `User` (`therapist_ID`)
            ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Tension
(
    tension_id INT AUTO_INCREMENT,
    rating     INT      NOT NULL,
    date       DATETIME NOT NULL,
    user_id    CHAR(11) NOT NULL,
    PRIMARY KEY (tension_id),
    FOREIGN KEY (user_id) REFERENCES User (user_id)
);

CREATE TABLE Mood
(
    mood_id INT AUTO_INCREMENT,
    rating  INT      NOT NULL,
    date    DATETIME NOT NULL,
    user_id CHAR(11) NOT NULL,
    PRIMARY KEY (mood_id),
    FOREIGN KEY (user_id) REFERENCES User (user_id)
);

CREATE TABLE Journal_entry
(
    journal_entry_id INT AUTO_INCREMENT,
    journal_entry    VARCHAR(1000) NOT NULL,
    date             DATETIME      NOT NULL,
    user_id          CHAR(11)      NOT NULL,
    PRIMARY KEY (journal_entry_id),
    FOREIGN KEY (user_id) REFERENCES User (user_id)
);

CREATE TABLE Facts
(
    fact_id    INT AUTO_INCREMENT UNIQUE,
    fact       VARCHAR(511) NOT NULL,
    last_shown DATETIME DEFAULt NULL,
    PRIMARY KEY (fact)
);



-- Function to determine time of the day from datetime => Morning, noon, afternoon, evening

/*DELIMITER $$
CREATE FUNCTION determineTimeOfDay(
entry_time DATETIME
)
RETURNS VARCHAR(20)
DETERMINISTIC
BEGIN
IF TIME(entry_time) >= '05:00:00' && TIME(entry_time) < '10:00:00' THEN
    RETURN ("1-Morning");
ELSEIF TIME(entry_time) >= '10:00:00' && TIME(entry_time) < '14:00:00' THEN
    RETURN ("2-Noon");
ELSEIF TIME(entry_time) >= '14:00:00' && TIME(entry_time) < '19:00:00' THEN
    RETURN ("3-Afternoon");
ELSEIF TIME(entry_time) >= '19:00:00' || TIME(entry_time) < '5:00:00' THEN
    RETURN ("4-Evening");
END IF;
END$$
DELIMITER ;
*/