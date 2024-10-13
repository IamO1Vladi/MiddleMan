using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;

namespace MiddleManServices.ApiServices.QuickBase.ServiceModels;

public class InformationThumbnailServiceModel
{
    [Required]
    [Url]
    public string ThumbnailImageLink { get; set; } = null!;

    [Required]
    [StringLength(ValidationConstants.MaxTopicLength, MinimumLength = ValidationConstants.MinTopicLength)]
    public string Topic { get; set; } = null!;

    [Required]
    [StringLength(ValidationConstants.MaxSummaryLength, MinimumLength = ValidationConstants.MinSummaryLength)]
    public string Summary { get; set; } = null!;
}