using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Infra.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public PersonGender Gender { get; set; }
        public string PersonalId { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Photo { get; set; }
        public int? CityId { get; set; }

        public City? City { get; set; }
        public List<PhoneNumber>? Phone { get; set; }
        public List<Connection>? Connection { get; set; }

    }
}
