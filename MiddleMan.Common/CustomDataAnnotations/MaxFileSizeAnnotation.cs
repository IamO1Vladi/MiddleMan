using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MiddleMan.Common.CustomDataAnnotations;

public class MaxFileSizeAnnotation: ValidationAttribute
{

    private readonly int maxFileSize;

    public MaxFileSizeAnnotation(int maxFileSize)
    {
        this.maxFileSize=maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        IFormFile? file = value as IFormFile;

        if (file != null)
        {
            if (file.Length > maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is {maxFileSize}Mb");
            }
        }

        return ValidationResult.Success;
    }
}