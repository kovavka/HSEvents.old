using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain
{
    public class ContactPerson: PersonEntity
    {
        public virtual string Appointment { get; set; }
    }
}
