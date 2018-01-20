using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;
using FluentNHibernate.Mapping;

namespace Domain
{
    public class Book:Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }

}
