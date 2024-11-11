using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Http;
using MiddleMan.Common.Constants;
using MiddleMan.Common.CustomDataAnnotations;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class GetInTouchServiceModel
{
    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchNameElementErrorMessage)]
    [StringLength(ValidationConstants.MaxGetInTouchNameLength, MinimumLength = ValidationConstants.MinGetInTOuchNameLength)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchEmailElementErrorMessage)]
    [EmailAddress(ErrorMessage = QuickBaseApiConstants.GetInTouchEmailElementFormatErrorMessage)]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = QuickBaseApiConstants.GetInTouchPhoneNumberElementErrorMessage)]
    public string? PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchServiceElementErrorMessage)]
    public string ServiceType { get; set; } = null!;

    [StringLength(ValidationConstants.MaxGetInTouchIndustryLength,MinimumLength = ValidationConstants.MinGetInTOuchIndustryLength)]
    public string? Industry { get; set; }

    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchMessageElementErrorMessage)]
    [StringLength(ValidationConstants.MaxGetInTouchMessageLength, MinimumLength = ValidationConstants.MinGetInTouchMessageLength)]
    public string InitialMessage { get; set; } = null!;

    [MaxFileSizeAnnotation(ValidationConstants.MaxFileAttachmentSize)]
    public IFormFile? FileAttachment { get; set; }
}