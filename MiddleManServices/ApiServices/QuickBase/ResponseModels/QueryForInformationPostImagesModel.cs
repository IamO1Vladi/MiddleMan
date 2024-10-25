using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class QueryForInformationPostImagesModel
{
    [Required]
    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    [Required]
    [JsonProperty("fileName")]
    public string FileName { get; set; } = null!;
}