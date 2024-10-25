using System.ComponentModel.DataAnnotations;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class InformationSinglePostServiceModel
{
    [Required]
    [Url]
    public string HeaderImageUrl { get; set; } = null!;

    [Required]
    public string? Category { get; set; }

    [Required]
    public string Topic { get; set; } = null!;

    [Required]
    public string FirstParagraph { get; set; } = null!;

    [Required]
    public string SecondParagraph { get; set; } = null!;

    [Required]
    public string SectionImageUrl { get; set; } = null!;

    [Required]
    public int PostViews { get; set; }

    public ICollection<string>? PostImages { get; set; }
}