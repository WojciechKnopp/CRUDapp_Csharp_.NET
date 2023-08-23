using System.ComponentModel.DataAnnotations;

namespace CookiesApp.Models;

public class Cookie : IValidatableObject
{
    public int Id { get; set; }
    
    
    [Required(ErrorMessage = "Podaj nazwę")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć 3-40 znaków")]
    [BigLetterStart("Nazwa")]
    public string Name { get; set; }
    
    
    [Required(ErrorMessage = "Podaj producenta")]
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    
    [Required(ErrorMessage = "Podaj alergeny")]
    [MinLength(3, ErrorMessage = "Lista alergenów musi mieć conajmniej 3 znaki")]
    [NoQuote]
    public string Allergens { get; set; }
    

    [Url(ErrorMessage = "Podaj poprawny adres URL")]
    public string? ImageUrl { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Name == "Zabronione" && ImageUrl == "http://zabroniony.com")
            yield return new ValidationResult(
                $"Ciastka o nazwie \"{Name}\" i zdjęciu pod adresem \"{ImageUrl}\" są zabronione na potrzeby zadania.");
    }
}