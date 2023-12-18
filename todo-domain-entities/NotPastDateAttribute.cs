using System;
using System.ComponentModel.DataAnnotations;

namespace todo_domain_entities
{
    public class NotPastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                // Only valid if the date is today or later
                return dateTime.Date >= DateTime.Now.Date;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "The date must be today or late";
        }
    }
}
