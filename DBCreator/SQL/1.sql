Create table SchoolType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table School(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Number int,
BelongToUniversityDistrict bit not null,
HasPriority bit not null,
TypeId int not null,

primary key (Id),
constraint FK_Schoole_SchoolType foreign key (TypeId) references SchoolType(Id)
);

Create table ContactPerson(
Id int identity(1,1) not null,
FullName nvarchar(255) not null,
PhoneNumber nvarchar(255),
Email nvarchar(255),
Appointment nvarchar(255),
SchoolId int not null,

primary key (Id),
constraint FK_ContactPerson_School foreign key (SchoolId) references School(Id)
);

