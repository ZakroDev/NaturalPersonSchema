using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Infra.Model
{
    public class Connection
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ConnectedPersonId {  get; set; }
        public ConnectedPersonType ConnectionType { get; set; }

        public Person? Person { get; set; }
    }
}
