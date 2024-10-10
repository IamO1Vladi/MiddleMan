using System.ComponentModel.DataAnnotations;

namespace MiddleMan.Models;

public class GetInTouchViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string ServiceType { get; set; } = null!;

    public string? Industry { get; set; }

    [Required]
    [StringLength(5000,MinimumLength = 10)]
    public string InitialMessage { get; set; } = null!;


}