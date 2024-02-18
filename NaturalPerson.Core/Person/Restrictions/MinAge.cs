﻿using System.ComponentModel.DataAnnotations;

namespace NaturalPerson.Core.Person.Restrictions
{
    public class MinAge : ValidationAttribute
    {
        private int _Limit;
        public MinAge(int Limit)
        {
            this._Limit = Limit;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime bday = DateTime.Parse(value.ToString());
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age))
            {
                age--;
            }
            if (age < _Limit)
            {
                ValidationResult result = new ValidationResult("Sorry you are not old enough");
                return result;
            }

            return null;
        }
    }
}
