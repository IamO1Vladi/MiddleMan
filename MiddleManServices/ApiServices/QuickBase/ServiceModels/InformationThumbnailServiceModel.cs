using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;
using MiddleManServices.ApiServices.QuickBase.ResponseModels;

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

    [Required]
    public Metadata Metadata { get; set; } = null!;
}