using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public interface IEvent : INamedEntity
    {
        EventType Type { get; set; }
        string Comment { get; set; }
        string Info { get; set; }
        Address Address { get; set; }
    }

    public class Event : NamedEntity, IEvent
    {
        public virtual EventType Type { get; set; }
        public virtual string Comment { get; set; }
        public virtual string Info { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }
        public virtual ICollection<EventDate> Dates { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
        public virtual ICollection<Employee> Organizers { get; set; }
        public virtual ICollection<Employee> Lecturers { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; }
    }

    public enum EventType
    {
        Course = 1,
        AcademicCompetition = 2,
        SchoolWork = 3
    }
}
