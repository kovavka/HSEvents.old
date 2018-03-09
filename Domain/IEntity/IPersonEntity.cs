using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IEntity
{
    public interface IPersonEntiny: IEntity
    {
        ContactInfo ContactInfo { get; set; }
    }

    public class PersonEntity : Entity, IPersonEntiny
    {
        public virtual ContactInfo ContactInfo { get; set; }
    }
}
