using System.ComponentModel.DataAnnotations;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class GetInTouchServiceModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Please select a service.")]
    public string ServiceType { get; set; } = null!;

    public string? Industry { get; set; }

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(5000, MinimumLength = 10)]
    public string InitialMessage { get; set; } = null!;
}