
Create table Country(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Region(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Country_Id int not null,
primary key (Id),
constraint FK_Region_Country foreign key (Country_Id) references Country(Id)
);

Create table CityType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Region_Id int not null,
primary key (Id),
constraint FK_CityType_Region foreign key (Region_Id) references Region(Id)
);

Create table City(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
CityType_Id int not null,
primary key (Id),
constraint FK_City_CityType foreign key (CityType_Id) references CityType(Id)
);

Create table Street(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
City_Id int not null,
primary key (Id),
constraint FK_Street_City foreign key (City_Id) references City(Id)
);

Create table House(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Street_Id int not null,
primary key (Id),
constraint FK_House_Street foreign key (Street_Id) references Street(Id)
);

Create table Address(
Id int identity(1,1) not null,
School_Id int,
House_Id int not null,
primary key (Id),
constraint FK_Address_School foreign key (School_Id) references School(Id),
constraint FK_Address_House foreign key (House_Id) references House(Id)
);