using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NaturalPerson.Core.Person.Restrictions
{
    public partial class FirstnameAndLastnameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace((string?)value);

            string input = (string)value;

            if (!LatinRegex().IsMatch(input) && !GeorgianRegex().IsMatch(input))
            {
                return new ValidationResult("Only Latin or georgian alphabets are allowed.");
            }

            return ValidationResult.Success!;
        }

        [GeneratedRegex("^[a-zA-Z]+$")]
        private static partial Regex LatinRegex();

        [GeneratedRegex("^[ა-ჰ]+$")]
        private static partial Regex GeorgianRegex();
    }
}
