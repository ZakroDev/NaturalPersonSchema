using NaturalPerson.Core.Person.Restrictions;
using System.ComponentModel.DataAnnotations;

namespace NaturalPerson.Core.Person
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        public required string PersonalId { get; set; }

        [Required]
        [FirstnameAndLastname]
        [StringLength(50, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [FirstnameAndLastname]
        [StringLength(50, MinimumLength = 2)]
        public required string LastName { get; set; }

        public PersonGender? Gender { get; set; }

        [MinAge(18)]
        public DateOnly BirthDate { get; set; }
        public PersonsCity? City { get; set; }
        public string? Photo { get; set; }
        public List<PhoneNumber>? Phone { get; set; }
        public List<Connection>? ConnectedPerson { get; set; }

    }
}
