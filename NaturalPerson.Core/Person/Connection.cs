using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Core.Person
{
    public class Connection
    {
        public int PersonId { get; set; }
        public int ConnectedPersonId { get; set; }

        public ConnectedPersonType ConnectionType { get; set; }
    }
}
