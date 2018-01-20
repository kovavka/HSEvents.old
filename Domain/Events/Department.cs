using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class Department : NamedEntity
    {
        public virtual string Color { get; set; }
    }
}
