using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Domain.IEntity;

namespace Domain
{
    public class Pupil : Attendee
    {
        public virtual Sex Sex { get; set; }
        public virtual int YearOfGraduation { get; set; }

        public virtual AcademicProgram EnterProgram { get; set; }
        public virtual ICollection<AcademicProgram> IntrestingPrograms { get; set; }
        public virtual ICollection<AcademicProgram> RegistrarionPrograms { get; set; }
        public virtual ICollection<СompetitionResult> СompetitionResults { get; set; }
    }
    
    public enum Sex
    {
        Male = 0,
        Female = 1
    }

    public class AcademicProgram : NamedEntity
    {
    }

}
