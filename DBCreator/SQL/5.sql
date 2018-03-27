﻿Create table Event(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
Info nvarchar(255) not null,
Comment nvarchar(255),
Type int not null,

primary key (Id)
);

Create table Subject(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Course(
Price money,
Duration int,
Subject_Id int not null,
Event_Id int not null,

constraint FK_Course_Subject foreign key (Subject_Id) references Subject(Id),
constraint FK_Course_Event foreign key (Event_Id) references Event(Id),
constraint AK_Course_EventId unique(Event_Id)   
);

Create table AcademicСompetition(
Subject_Id int not null,
Event_Id int not null,

constraint FK_AcademicСompetition_Subject foreign key (Subject_Id) references Subject(Id),
constraint FK_AcademicСompetition_Event foreign key (Event_Id) references Event(Id),
constraint AK_AcademicСompetition_EventId unique(Event_Id)   
);


Create table SchoolWork(
Program text,
Event_Id int not null,

constraint FK_SchoolWork_Event foreign key (Event_Id) references Event(Id),
constraint AK_SchoolWork_EventId unique(Event_Id)   
);

Create table EventExecution(
Id int identity(1,1) not null,
Address_Id int not null,
Event_Id int,

primary key (Id),
constraint FK_EventExecution_Address foreign key (Address_Id) references Address(Id),
constraint FK_EventExecution_Event foreign key (Event_Id) references Event(Id)
);

Create table EventDate(
Id int identity(1,1) not null,
Date date not null,
StartTime time,
EndTime time,
EventExecution_Id int,

primary key (Id),
constraint FK_EventDate_EventExecution foreign key (EventExecution_Id) references EventExecution(Id)
);

Create table Purchase(
Id int identity(1,1) not null,
Price money not null,
Description nvarchar(255) not null,
Event_Id int,

primary key (Id),
constraint FK_Purchase_Event foreign key (Event_Id) references Event(Id)
);
