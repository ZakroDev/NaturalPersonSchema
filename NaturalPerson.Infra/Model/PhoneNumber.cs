using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Infra.Model
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string? Phone { get; set; }
        public PhoneType Type { get; set; }

        public Person? People { get; set; }
    }
}
