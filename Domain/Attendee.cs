using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Domain.IEntity;

namespace Domain
{
    public class Attendee : PersonEntity
    {
        public virtual AttendeeType Type { get; set; }
    }

    public enum AttendeeType
    {
        [Description("Абитуриент")] Pupil = 1,
        [Description("Родитель")] Parent = 2,
        [Description("Учитель")] Teacher = 3,
    }

    public class AttendanceInfo : Entity
    {
        public virtual Attendee Attendee { get; set; }
        public virtual bool Participated { get; set; }
    }

}
