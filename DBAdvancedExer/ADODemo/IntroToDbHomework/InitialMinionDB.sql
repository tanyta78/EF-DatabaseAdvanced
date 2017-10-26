CREATE DATABASE MinionsDB

CREATE TABLE Towns(
TownId INT IDENTITY(1,1) PRIMARY KEY,
TownName VARCHAR(50),
Country VARCHAR(50) 
)

CREATE TABLE Minions(
MinionId INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(50),
Age INT,
TownId INT FOREIGN KEY REFERENCES Towns(TownId)
)

CREATE TABLE Villains(
VillainId INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(50),
EvilFactor VARCHAR(15),
CHECK (EvilFactor IN ('good','bad','evil','super evil'))
)

CREATE TABLE VillainsMinions(
VillainId INT FOREIGN KEY REFERENCES Villains(VillainId),
MinionId INT FOREIGN KEY REFERENCES Minions(MinionId),
CONSTRAINT PK_VillMin PRIMARY KEY (VillainId,MinionId)
)

INSERT INTO Towns VALUES
('Sofia','BG'),('Varna','BG'),('Plovdiv','BG'),('Ruse','BG'),('Veliko Turnovo','BG'),('Burgas','BG')

INSERT INTO Minions VALUES
('Bob',12,1),
('Kevin',15,1),
('Gosho',10,2),
('Pesho',8,3),
('Simon',10,4),
('Poppy',12,5),
('Jimmy',12,1)

INSERT INTO Villains VALUES
('Gru','good'),
('Katty','evil'),
('Hall','super evil'),
('Mond','bad'),
('Blue','good')

INSERT INTO VillainsMinions VALUES
(1,1),
(1,2),
(1,3),
(1,4),
(1,5),
(2,6),
(2,7),
(2,1),
(2,3),
(3,2),
(3,3),
(3,5),
(4,7)


DROP database MinionsDB