using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class Spending : Entity
    {
        public virtual decimal Cost { get; set; }
        public virtual string Purchase { get; set; }
    }
}
