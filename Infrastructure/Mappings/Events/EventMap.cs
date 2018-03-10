using Domain;
using Domain.Events;
using Domain.IEntity;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infrastructure.Mappings.Events
{
    class EventMap : NamedEntityMap<Event>
    {
        public EventMap()
        {
            Map(x => x.Comment).Nullable();
            Map(x => x.Info);
            Map(x => x.Type);

            HasManyToMany(x => x.Volunteers).AsBag().Cascade.SaveUpdate().Table("VolunteerInfo");

            HasMany(x => x.Purchases).AsBag().Cascade.SaveUpdate().ForeignKeyConstraintName("FK_Purchase_Event");
            HasManyToMany(x => x.Organizers).AsBag().Cascade.SaveUpdate().Table("Organizer");
            HasManyToMany(x => x.Lecturers).AsBag().Cascade.SaveUpdate().Table("Lecturer");
            HasManyToMany(x => x.Departments).AsBag().Cascade.SaveUpdate().Table("DepartmentInfo");
            HasMany(x => x.Attendees).AsBag().Cascade.SaveUpdate().ForeignKeyConstraintName("FK_AttendanceInfo_Event");
            HasMany(x => x.EventExecution).AsBag().Cascade.SaveUpdate().ForeignKeyConstraintName("FK_EventExecution_Event");
        }
    }
    
    class CourseMap : SubclassMap<Course>
    {
        public CourseMap()
        {
            Map(x => x.Price).Nullable();
            Map(x => x.Duration).Nullable();
            References(x => x.Subject).Cascade.SaveUpdate().ForeignKey("FK_Course_Subject");
        }
    }

    class AcademicСompetitionMap : SubclassMap<AcademicСompetition>
    {
        public AcademicСompetitionMap()
        {
            References(x => x.Subject).Cascade.SaveUpdate().ForeignKey("FK_AcademicСompetition_Subject");
        }
    }

    class SchoolWorkMap : SubclassMap<SchoolWork>
    {
        public SchoolWorkMap()
        {
            Map(x => x.Program).Nullable();
        }
    }

    class SubjectMap : NamedEntityMap<Subject>
    {
        public SubjectMap()
        {
        }
    }

    class DepartmentMap : NamedEntityMap<Department>
    {
        public DepartmentMap()
        {
            Map(x => x.Color);
        }
    }
    class EmployeeMap : EntityMap<Employee>
    {
        public EmployeeMap()
        {
            Component(x => x.ContactInfo,
                c =>
                {
                    c.Map(x => x.FullName);
                    c.Map(x => x.Email).Nullable();
                    c.Map(x => x.PhoneNumber).Nullable();
                });
            Map(x => x.Appointment);
        }
    }
    class EventExecutionMap : EntityMap<EventExecution>
    {
        public EventExecutionMap()
        {
            HasMany(x => x.Dates).AsBag().Cascade.SaveUpdate().ForeignKeyConstraintName("FK_EventDate_EventExecution");
            References(x => x.Address).Cascade.SaveUpdate().ForeignKey("FK_EventExecution_Address");
        }
    }
    class EventDateMap : EntityMap<EventDate>
    {
        public EventDateMap()
        {
            Map(x => x.Date);
            Map(x => x.StartTime).CustomType("TimeAsTimeSpan");
            Map(x => x.EndTime).CustomType("TimeAsTimeSpan");
        }
    }
    class PurchaseMap : EntityMap<Purchase>
    {
        public PurchaseMap()
        {
            Map(x => x.Price);
            Map(x => x.Description);
        }
    }
    class AttendanceInfoMap : EntityMap<AttendanceInfo>
    {
        public AttendanceInfoMap()
        {
            Map(x => x.Participated);
            References(x => x.Attendee).Cascade.SaveUpdate().ForeignKey("FK_AttendanceInfo_Attendee");
        }
    }

}
