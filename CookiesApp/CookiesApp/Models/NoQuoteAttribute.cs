using System.ComponentModel.DataAnnotations;

namespace CookiesApp.Models;

public class NoQuoteAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string str = value.ToString();
            if (str.StartsWith("\"") || str.EndsWith("\"") || str.StartsWith("\'") || str.EndsWith("\'"))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        return ValidationResult.Success;
    }
    
    private string GetErrorMessage()
    {
        return "Usuń cudzysłowy z początku i końca tekstu";
    }
    
}