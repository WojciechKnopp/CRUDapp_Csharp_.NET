using System.ComponentModel.DataAnnotations;

namespace CookiesApp.Models;

public class BigLetterStartAttribute: ValidationAttribute
{
    private string _name;
    
    public BigLetterStartAttribute(string name)
    {
        _name = name;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string str = value.ToString();
            if (char.IsLower(str[0]))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        return ValidationResult.Success;
    }
    
    private string GetErrorMessage()
    {
        return $"{_name} musi zaczynać się wielką literą";
    }

}
