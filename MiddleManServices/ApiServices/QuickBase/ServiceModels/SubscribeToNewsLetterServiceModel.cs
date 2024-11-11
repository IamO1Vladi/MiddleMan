using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class SubscribeToNewsLetterServiceModel
{
    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchNameElementErrorMessage)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = QuickBaseApiConstants.GetInTouchEmailElementErrorMessage)]
    [EmailAddress(ErrorMessage = QuickBaseApiConstants.GetInTouchEmailElementFormatErrorMessage)]
    public string Email { get; set; } = null!;
}