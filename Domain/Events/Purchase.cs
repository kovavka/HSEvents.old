using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain.Events
{
    public class Purchase : Entity
    {
        public virtual decimal Price { get; set; }
        public virtual string Description { get; set; }
    }
}
