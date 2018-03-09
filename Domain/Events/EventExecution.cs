using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class EventDate : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual TimeSpan StartTime { get; set; }
        public virtual TimeSpan EndTime { get; set; }
    }

    public class EventExecution : Entity
    {
        public virtual ICollection<EventDate> Dates { get; set; }
        public virtual Address Address { get; set; }
    }
}
