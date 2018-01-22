
Create table Country(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Region(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
CountryId int not null,
primary key (Id),
constraint FK_Region_Country foreign key (CountryId) references Country(Id)
);

Create table City(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
RegionId int not null,
primary key (Id),
constraint FK_City_Region foreign key (RegionId) references Region(Id)
);

Create table CityType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
CityId int not null,
primary key (Id),
constraint FK_CityType_City foreign key (CityId) references City(Id)
);

Create table Street(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
CityTypeId int not null,
primary key (Id),
constraint FK_Street_CityType foreign key (CityTypeId) references CityType(Id)
);

Create table House(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
StreetId int not null,
primary key (Id),
constraint FK_House_Street foreign key (StreetId) references Street(Id)
);

Create table Address(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
SchoolId int,
primary key (Id),
constraint FK_Addressn_School foreign key (SchoolId) references School(Id)
);