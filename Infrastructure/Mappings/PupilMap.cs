using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Events;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings
{
    class PupilMap : SubclassMap<Pupil>
    {
        public PupilMap()
        {
            Map(x => x.Sex);
            Map(x => x.YearOfGraduation);
            References(x => x.School).Cascade.SaveUpdate().ForeignKey("FK_Pupil_School");
            References(x => x.EnterProgram).Cascade.SaveUpdate().ForeignKey("FK_Pupil_EnterProgram");
            HasManyToMany(x => x.IntrestingPrograms).AsBag().Cascade.SaveUpdate().Table("IntrestingProgram");
            HasManyToMany(x => x.RegistrarionPrograms).AsBag().Cascade.SaveUpdate().Table("RegistrarionProgram");
        }
    }

    class ContactPersonMap : PersonMap<ContactPerson>
    {
        public ContactPersonMap()
        {
            Map(x => x.Appointment).Nullable();
        }
    }

    class AcademicProgramMap : NamedEntityMap<AcademicProgram>
    {
    }

}
