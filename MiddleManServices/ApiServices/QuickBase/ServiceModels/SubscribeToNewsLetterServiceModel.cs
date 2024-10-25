using System.ComponentModel.DataAnnotations;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class SubscribeToNewsLetterServiceModel
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}