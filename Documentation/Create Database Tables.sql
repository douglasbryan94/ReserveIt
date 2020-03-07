CREATE TABLE Discounts (
  DiscountID varchar(10) PRIMARY KEY,
  DiscountDescription varchar(255) NOT NULL,
  DiscountPercentage decimal(4,2) NOT NULL
)


CREATE TABLE Managers (
  ManagerID int IDENTITY(1, 1) PRIMARY KEY,
  Firstname varchar(20) NOT NULL,
  Middlename varchar(20) DEFAULT NULL,
  Lastname varchar(20) NOT NULL
)

CREATE TABLE Hotels (
  HotelID int IDENTITY(1, 1) PRIMARY KEY,
  ManagerID int DEFAULT NULL,
  MaxCapacity int NOT NULL,
  StreetAddress varchar(30) NOT NULL,
  CityAddress varchar(20) NOT NULL,
  StateAddress varchar(20) NOT NULL,
  CountryAddress varchar(20) NOT NULL,
  ZIPAddress varchar(10) NOT NULL,
  Phone varchar(11) NOT NULL,
  Description varchar(700) NOT NULL,

  CONSTRAINT FK_ManagerID FOREIGN KEY (ManagerID)
	REFERENCES Managers (ManagerID) ON DELETE SET NULL ON UPDATE CASCADE
)

CREATE TABLE RoomTypes (
  RoomTypeID varchar(5) PRIMARY KEY,
  RoomTypeDescription varchar(30) NOT NULL,
  MaxOccupancy int NOT NULL
)

CREATE TABLE Rooms (
  RoomID int IDENTITY(1, 1) PRIMARY KEY,
  HotelID int NOT NULL,
  RoomTypeID varchar(5) NOT NULL,
  RoomNumber int NOT NULL,
  CurrentRate decimal(15,2) NOT NULL,

  CONSTRAINT FK_HotelID FOREIGN KEY (HotelID)
  REFERENCES Hotels (HotelID) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_RoomTypeID FOREIGN KEY (RoomTypeID)
  REFERENCES RoomTypes (RoomTypeID) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE UserLevels (
  UserLevelID int PRIMARY KEY,
  UserLevelDescription varchar(15) NOT NULL
)

CREATE TABLE Users (
  UserID int IDENTITY(1, 1) PRIMARY KEY,
  Email varchar(50) NOT NULL,
  Password varchar(30) NOT NULL,
  UserLevel int DEFAULT 2,
  Firstname varchar(20) NOT NULL,
  Middlename varchar(20) DEFAULT NULL,
  Lastname varchar(20) NOT NULL,
  StreetAddress varchar(30) NOT NULL,
  CityAddress varchar(20) NOT NULL,
  StateAddress varchar(20) NOT NULL,
  CountryAddress varchar(20) NOT NULL,
  ZIPAddress varchar(10) NOT NULL,
  Phone varchar(11) NOT NULL,

  CONSTRAINT FK_UserLevel FOREIGN KEY (UserLevel)
  REFERENCES UserLevels (UserLevelID) ON DELETE SET NULL ON UPDATE CASCADE
)

CREATE TABLE Reservations (
  ReservationID int IDENTITY(1, 1) PRIMARY KEY,
  UserID int DEFAULT NULL,
  RoomID int DEFAULT NULL,
  CheckIn date NOT NULL,
  StayLength int NOT NULL,
  NightlyRate decimal(15,2) NOT NULL,
  DiscountID varchar(10) DEFAULT NULL,
  
  CONSTRAINT FK_UserID FOREIGN KEY (UserID)
  REFERENCES Users (UserID) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_RoomID FOREIGN KEY (RoomID)
  REFERENCES Rooms (RoomID) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_DiscountID FOREIGN KEY (DiscountID)
  REFERENCES Discounts (DiscountID) ON DELETE NO ACTION ON UPDATE CASCADE
)