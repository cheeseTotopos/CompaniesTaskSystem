CREATE DATABASE tasksSystem;
USE tasksSystem;

CREATE TABLE Companies(
	Id int NOT NULL AUTO_INCREMENT,
    CompanyName varchar(255),
    Pwd varchar(255),
    IsActive int,
    PRIMARY KEY (Id)
);

CREATE TABLE Users(
	Id int NOT NULL AUTO_INCREMENT,
    FullName varchar(255),
    Email varchar(255),
    CreatedAt date,
    Companie int,
    PRIMARY KEY (Id),
    FOREIGN KEY (Companie) REFERENCES Companies(Id)
);

CREATE TABLE TaskStatus(
	Id int NOT NULL AUTO_INCREMENT,
    StatusName varchar(255),
    IsActive int,
    Companie int,
    CreatedBy int,
    
    PRIMARY KEY (Id),
    FOREIGN KEY (Companie) REFERENCES Companies(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);

CREATE TABLE Tasks(
	Id int NOT NULL AUTO_INCREMENT,
    Title varchar(255),
    TaskDescription varchar(255),
    CompanyId int,
    StatusId int,
    IsPriority int,
    DueDate date,
    CreatedBy int,
    UserId int,
    
    PRIMARY KEY (Id),
    FOREIGN KEY (CompanyId) REFERENCES Copmanies(Id),
    FOREIGN KEY (StatusId) REFERENCES TaskStatus(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);