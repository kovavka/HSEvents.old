using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class Volunteer : Entity
    {
        public virtual string FullName { get; set; }
        public virtual Group Group { get; set; }
    }

    public class Group : NamedEntity
    {
        
    }
}
