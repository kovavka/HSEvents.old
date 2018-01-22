Create table Attendee(
Id int identity(1,1) not null,
FullName nvarchar(255) not null,
PhoneNumber nvarchar(255),
Email nvarchar(255),
Type int not null,

primary key (Id)
);

Create table AcademicProgram(
Id int identity(1,1) not null,
Name nvarchar(255) not null,
primary key (Id)
);	

Create table Pupil(
Sex int not null,
YearOfGraduation int not null,
EnterProgramId int,
SchoolId int not null,
AttendeeId int not null,

constraint FK_Pupil_EnterProgram foreign key (EnterProgramId) references AcademicProgram(Id),
constraint FK_Pupil_School foreign key (SchoolId) references School(Id),
constraint FK_Pupil_Attendee foreign key (AttendeeId) references Attendee(Id),
constraint AK_AttendeeId unique(AttendeeId)
);

Create table IntrestingProgram(
AcademicProgramId int not null,
PupilId int not null,

constraint FK_IntrestingProgram_AcademicProgram foreign key (AcademicProgramId) references AcademicProgram(Id),
constraint FK_IntrestingProgram_Pupil foreign key (PupilId) references Pupil(AttendeeId)

);	

Create table RegistrarionProgram(
AcademicProgramId int not null,
PupilId int not null,

constraint FK_RegistrarionProgram_AcademicProgram foreign key (AcademicProgramId) references AcademicProgram(Id),
constraint FK_RegistrarionProgram_Pupil foreign key (PupilId) references Pupil(AttendeeId)
);