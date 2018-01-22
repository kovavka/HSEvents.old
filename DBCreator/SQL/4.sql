Create table EventType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);


Create table Event(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Info nvarchar(255) not null,
Comment nvarchar(255),
TypeId int not null,
AddressId int not null,

primary key (Id),
constraint FK_Event_EventType foreign key (TypeId) references EventType(TypeId),
constraint FK_Event_Address foreign key (AddressId) references Address(AddressId),
);
