using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Domain.IEntity;

namespace Domain
{
    public class СompetitionResult : Entity
    {
        public virtual AcademicСompetition AcademicСompetition { get; set; }
        public virtual int NumberOfPoints { get; set; }
        public virtual ResultType Result { get; set; }
    }

    public class ResultType : NamedEntity
    {
    }
}
