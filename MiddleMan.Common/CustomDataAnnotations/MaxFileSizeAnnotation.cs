using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MiddleMan.Common.Constants;

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
                return new ValidationResult(string.Format(TextConstants.MaxFileSizeErrorMessage,maxFileSize));
            }
        }

        return ValidationResult.Success;
    }
}