using System.ComponentModel.DataAnnotations;

namespace MiddleMan.Web.ViewModels.InformationSection;

public class InformationSinglePostViewModel
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

    public ICollection<string>? PostImages { get; set; }
}