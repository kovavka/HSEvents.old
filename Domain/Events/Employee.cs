using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class Employee : Entity
    {
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual string Appointment { get; set; }
    }
}
