using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class QueryForInformationPostImagesModel
{
    [Required]
    [JsonProperty(QuickBaseApiConstants.QueryForPostImagesUrlElement)]
    public string Url { get; set; } = null!;

    [Required]
    [JsonProperty(QuickBaseApiConstants.QueryForPostImagesFileNameElement)]
    public string FileName { get; set; } = null!;
}