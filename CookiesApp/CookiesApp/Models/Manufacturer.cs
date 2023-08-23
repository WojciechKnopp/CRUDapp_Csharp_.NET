using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CookiesApp.Models;

public class Manufacturer : IValidatableObject
{
    public int Id { get; set; }
    
    
    [Required(ErrorMessage = "Podaj nazwę")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków")]
    [BigLetterStart("Nazwa")]
    public string Name { get; set; }
    
    
    [Required(ErrorMessage = "Podaj rok założenia")]
    [Year]
    public int YearFounded { get; set; }
    
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Name == "Zabroniony" && YearFounded == 2000)
        {
            yield return new ValidationResult(
                $"Producent \"{Name}\" założony w roku {YearFounded} jest zabroniony na potrzeby tego zadania",
                new[] { "Name", "YearFounded" });
        }
    }
}