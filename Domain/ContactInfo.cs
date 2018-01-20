using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContactInfo
    {
        public virtual string FullName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
    }
}
