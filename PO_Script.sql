Create Table Persons
(
	id int identity(1,1) primary key,
	firstName varchar(50) not null,
	middleName varchar(50) null,
	lastName varchar(50) not null,
	dateOfBirth Date not null,
	creationDate DateTime not null,
	gender varchar(50) not null,
	salutation varchar(10) not null,
	mobNo varchar(20) null,
	address varchar(250) null
)

Create Table Users
(
	id int identity(1,1) primary key,
	email varchar(100) not null,
	password varchar(100) not null,
	creationDate DateTime not null,
	personId int not null foreign key references Persons(id)
)

Select * from Persons
Select * from Users

--Alter Table Users
--drop constraint FK__Users__personId__300424B4

--ALTER TABLE Users
--ADD CONSTRAINT FK_Users_Persons
--FOREIGN KEY (personId) REFERENCES Persons(id);


Create table PurchaseOrders
(
	code varchar(10) primary key,
	orderDate datetime not null,
	vendorName varchar(200) not null,
	totalQty int not null,
	totalAmt money not null,
	comments varchar(500) null,
	remarks varchar(300) null
)

Create table PO_Items
(
	id int identity(1,1) primary key,
	itemName varchar(200) not null,
	po_Code varchar(10) not null foreign key references PurchaseOrders(code),
	unit varchar(10) not null,
	quantity int not null,
	rate money not null,
	amount money not null
)

Select * from PurchaseOrders
Select * from PO_Items

--Alter Table PO_Items
--drop constraint FK__PO_Items__po_Cod__4BAC3F29

--ALTER TABLE PO_Items
--ADD CONSTRAINT FK_PO_Items_PurchaseOrders
--FOREIGN KEY (po_Code) REFERENCES PurchaseOrders(code);

-- Use Ctrl + R  --> to open/close Result window


Create Table Countries
(
	Code varchar(5) primary key,
	Name varchar(20) not null
)

Insert into Countries(Code, Name) values
('+91', 'India'),
('+1', 'USA'),
('+86', 'China')
Select * from Countries


Create Table States
(
	Id int identity(1,1) primary key,
	Name varchar(50) not null,
	CountryCode varchar(5) not null foreign key references Countries(Code)
)

Insert into States(Name, CountryCode) values
('Delhi', '+91'),
('Uttar Pradesh', '+91'),
('Alaska', '+1'),
('Arizona', '+1'),
('Shanghai','+86'),
('Beijing','+86')
Select * from States


Create Table Districts
(
	Id int identity(1,1) primary key,
	Name varchar(50) not null,
	StateId int not null foreign key references States(Id)
)

Insert into Districts(Name, StateId) values
('Central Delhi', 1),
('East Delhi', 1),
('Jaunpur', 2),
('Varanasi', 2),
('District 1', 3),
('District 2', 3),
('District 1', 4),
('District 2', 4),
('District 1', 5),
('District 2', 5),
('District 1', 6),
('District 2', 6)
Select * from Districts


Create Table IDCardTypes
(
	Id int identity(1,1) primary key,
	Name varchar(50) not null
)

Insert into IDCardTypes(Name) values
('PAN'),
('Driving License'),
('Aadhar')
Select * from IDCardTypes


Create Table Candidates
(
	Id int identity(1,1) primary key,
	Name varchar(50) not null,
	FatherName varchar(50) not null,
	MotherName varchar(50) not null,
	DateOfBirth date not null,
	PermanentAddress varchar(200) not null,
	CorrespondingAddress varchar(200) not null,
	CountryCode varchar(5) not null foreign key references Countries(Code),
	StateId int not null foreign key references States(Id),
	DistrictId int not null foreign key references Districts(Id),
	PinCode int not null,
	MobileNo varchar(10) not null,
	IDCardTypeId int not null foreign key references IDCardTypes(Id),
	IDCardDetails varchar(50) not null
)
Select * from Candidates

Alter Table Candidates
Add Photo varbinary(max) not null;
