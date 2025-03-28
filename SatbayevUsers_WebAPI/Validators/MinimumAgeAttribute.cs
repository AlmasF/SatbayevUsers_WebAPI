﻿using System.ComponentModel.DataAnnotations;

namespace SatbayevUsers_WebAPI.Validators
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        public MinimumAgeAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;
            ErrorMessage = "{0} must be someone at least {1} years of age";
        }

        public override bool IsValid(object value)
        {
            if (value == null)  //you can just add this condition
            {
                return true;
            }

            DateTime date;

            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MinimumAge) < DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MinimumAge);
        }


        public int MinimumAge { get; }
    }
}
