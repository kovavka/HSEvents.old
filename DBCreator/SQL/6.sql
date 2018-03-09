﻿Create table VolunteerInfo(
Volunteer_Id int not null,
Event_Id int not null,

constraint FK_VolunteerInfo_Volunteer foreign key (Volunteer_Id) references Volunteer(Id),
constraint FK_VolunteerInfo_Event foreign key (Event_Id) references Event(Id)
);

Create table DepartmentInfo(
Department_Id int not null,
Event_Id int not null,

constraint FK_DepartmentInfo_Department foreign key (Department_Id) references Department(Id),
constraint FK_DepartmentInfo_Event foreign key (Event_Id) references Event(Id)
);

Create table Lecturer(
Employee_Id int not null,
Event_Id int not null,

constraint FK_Lecturer_Employee foreign key (Employee_Id) references Employee(Id),
constraint FK_Lecturer_Event foreign key (Event_Id) references Event(Id)
);

Create table Organizer(
Employee_Id int not null,
Event_Id int not null,

constraint FK_Organizer_Employee foreign key (Employee_Id) references Employee(Id),
constraint FK_Organizer_Event foreign key (Event_Id) references Event(Id)
);

Create table ResultType(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);

Create table Result(
Id int identity(1,1) not null,
NumberOfPoints int not null,
Type_Id int not null,
AcademicСompetition_Id int not null,
Attendee_Id int not null,

primary key (Id),
constraint FK_Result_ResultType foreign key (Type_Id) references ResultType(Id),
constraint FK_Result_AcademicСompetition foreign key (AcademicСompetition_Id) references AcademicСompetition(Event_Id),
constraint FK_Result_Attendee foreign key (Attendee_Id) references Attendee(Id)
);

Create table AttendanceInfo(
Id int identity(1,1) not null,
Participated bit not null,
Event_Id int not null,
Attendee_Id int not null,

primary key (Id),
constraint FK_AttendanceInfo_Event foreign key (Event_Id) references Event(Id),
constraint FK_AttendanceInfo_Attendee foreign key (Attendee_Id) references Attendee(Id)
);
