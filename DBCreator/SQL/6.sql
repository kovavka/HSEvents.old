Create table VolunteerInfo(
VolunteerId int not null,
EventId int not null,

constraint FK_VolunteerInfo_Volunteer foreign key (VolunteerId) references Volunteer(Id),
constraint FK_VolunteerInfo_Event foreign key (EventId) references Event(Id)
);

Create table DepartmentInfo(
DepartmentId int not null,
EventId int not null,

constraint FK_DepartmentInfo_Department foreign key (DepartmentId) references Department(Id),
constraint FK_DepartmentInfo_Event foreign key (EventId) references Event(Id)
);

Create table Lecturer(
EmployeeId int not null,
EventId int not null,

constraint FK_Lecturer_Employee foreign key (EmployeeId) references Employee(Id),
constraint FK_Lecturer_Event foreign key (EventId) references Event(Id)
);

Create table Organizer(
EmployeeId int not null,
EventId int not null,

constraint FK_Organizer_Employee foreign key (EmployeeId) references Employee(Id),
constraint FK_Organizer_Event foreign key (EventId) references Event(Id)
);

Create table ResultType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Result(
NumberOfPoints int not null,
TypeId int not null,
AcademicСompetitionId int not null,
AttendeeId int not null,

constraint FK_Result_ResultType foreign key (TypeId) references ResultType(Id),
constraint FK_Result_AcademicСompetition foreign key (AcademicСompetitionId) references AcademicСompetition(EventId),
constraint FK_Result_Attendee foreign key (AttendeeId) references Attendee(Id)
);

Create table AttendanceInfo(
Participated bit not null,
EventId int not null,
AttendeeId int not null,

constraint FK_AttendanceInfo_Event foreign key (EventId) references Event(Id),
constraint FK_AttendanceInfo_Attendee foreign key (AttendeeId) references Attendee(Id)
);
