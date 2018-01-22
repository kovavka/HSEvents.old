Create table Event(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Info nvarchar(255) not null,
Comment nvarchar(255),
Type int not null,
AddressId int not null,

primary key (Id),
constraint FK_Event_Address foreign key (AddressId) references Address(Id)
);

Create table Subject(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Course(
Cost money,
Duration int,
SubjectId int not null,
EventId int not null,

constraint FK_Course_Subject foreign key (SubjectId) references Subject(Id),
constraint FK_Course_Event foreign key (EventId) references Event(Id),
constraint AK_Course_EventId unique(EventId)   
);

Create table AcademicСompetition(
SubjectId int not null,
EventId int not null,

constraint FK_AcademicСompetition_Subject foreign key (SubjectId) references Subject(Id),
constraint FK_AcademicСompetition_Event foreign key (EventId) references Event(Id),
constraint AK_AcademicСompetition_EventId unique(EventId)   
);


Create table SchoolWork(
Program text,
EventId int not null,

constraint FK_SchoolWork_Event foreign key (EventId) references Event(Id),
constraint AK_SchoolWork_EventId unique(EventId)   
);

Create table EventDate(
Id int identity(1,1) not null,
Date date not null,
StartTime time,
EndTime time,
EventId int not null,

primary key (Id),
constraint FK_EventDate_Event foreign key (EventId) references Event(Id)
);

Create table Spending(
Id int identity(1,1) not null,
Cost money not null,
Purchase nvarchar(255) not null,
EventId int not null,

primary key (Id),
constraint FK_Spending_Event foreign key (EventId) references Event(Id)
);
