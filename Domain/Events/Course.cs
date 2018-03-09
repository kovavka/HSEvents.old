using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Events;
using Domain.IEntity;

namespace Domain.Events
{
    public class Course: Event
    {
        public virtual decimal? Price { get; set; }
        public virtual int? Duration { get; set; }
        public virtual Subject Subject { get; set; }    
    }
}
