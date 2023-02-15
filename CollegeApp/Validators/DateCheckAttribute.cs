using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators;

public class DateCheckAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var date = (DateTime)value;

        if (date < DateTime.Now)
        {
            return new ValidationResult("tarih bugunun tarihinden buyuk veya esit olmalidir");
        }

        return ValidationResult.Success;

    }
} 