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
            References(x => x.Address).Cascade.SaveUpdate().ForeignKey("FK_Event_Address");

            HasManyToMany(x => x.Volunteers).AsBag().Cascade.SaveUpdate().Table("VolunteerInfo");
            HasMany(x => x.Dates).AsBag().Cascade.SaveUpdate();
        }
    }
    
    class CourseMap : SubclassMap<Course>
    {
        public CourseMap()
        {
            Map(x => x.Cost).Nullable();
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
    class EventDateMap : EntityMap<EventDate>
    {
        public EventDateMap()
        {
            Map(x => x.Date);
            Map(x => x.StartTime).CustomType("TimeAsTimeSpan"); ;
            Map(x => x.EndTime).CustomType("TimeAsTimeSpan"); ;
        }
    }
    class SpendingMap : EntityMap<Spending>
    {
        public SpendingMap()
        {
            Map(x => x.Cost);
            Map(x => x.Purchase);
        }
    }

}
