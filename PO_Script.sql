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
