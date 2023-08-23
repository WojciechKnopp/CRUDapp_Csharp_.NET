using System.ComponentModel.DataAnnotations;

namespace CookiesApp.Models;

public class YearAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            int year = Int32.Parse(value.ToString());
            if(year > DateTime.Now.Year)
                return new ValidationResult($"Rok nie może być większy niż obecny rok: {DateTime.Now.Year}.");
            
            if (year < 1)
                return new ValidationResult("Rok nie może być mniejszy niż 1.");
            
        }
        return ValidationResult.Success;
    }
}