using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForDataSortBy
{
    [Required]
    [JsonProperty("fieldId")]
    public int FieldId { get; set; }

    [Required]
    [JsonProperty("order")]
    public string Order { get; set; } = null!;
}