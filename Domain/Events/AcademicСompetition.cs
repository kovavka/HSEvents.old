﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class AcademicСompetition : Event
    {
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
