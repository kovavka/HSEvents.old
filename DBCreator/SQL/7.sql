﻿Insert into Country (Name) values ('Россия');
Insert into Region (Name, Country_Id) values ('Пермский край', 1);
Insert into CityType (Name, Region_Id) values ('город', 1);
Insert into City (Name, CityType_Id) values ('Пермь', 1);
Insert into Street (Name, City_Id) values ('ул. Уральская', 1);
Insert into House (Name, Street_Id) values ('37', 1);
Insert into Address (House_Id) values (1);



Insert into Subject (Name) values ('Экономика');
Insert into Event (Name,Info,Comment,Type) values ('Экономика простыми словами', 'Курсы для школьников','Добавить описание', 1);
Insert into Course (Cost, Duration, Subject_Id, Event_Id) values (1500, 10, 1, 1);
Insert into EventExecution (Event_Id, Address_Id) values (1, 1);
Insert into EventDate (Date,StartTime,EndTime,EventExecution_Id) values ('2018-02-01', '14:30', '15:50', 1);
Insert into EventDate (Date,StartTime,EndTime,EventExecution_Id) values ('2018-02-03', '14:30', '15:50', 1);
Insert into EventDate (Date,StartTime,EndTime,EventExecution_Id) values ('2018-02-07', '14:30','15:50', 1);

Insert into Event (Name,Info,Type) values ('Экономика простыми словами', 'Курсы для школьников', 2);
Insert into AcademicСompetition (Subject_Id, Event_Id) values (1, 2);
Insert into EventExecution (Event_Id, Address_Id) values (2, 1);
Insert into EventDate (Date,StartTime,EndTime,EventExecution_Id) values ('2018-02-13', '14:30', '12:50', 2);

Insert into Event (Name,Info,Type) values ('Вышка в школы', 'Выезды в школы с академ руководителями', 3);
Insert into SchoolWork (Program, Event_Id) values ('Программа', 3);
Insert into EventExecution (Event_Id, Address_Id) values (3, 1);
Insert into EventDate (Date,StartTime,EndTime,EventExecution_Id) values ('2018-02-5', '14:30', '15:50', 3);

