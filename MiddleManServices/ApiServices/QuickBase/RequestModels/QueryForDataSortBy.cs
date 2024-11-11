using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForDataSortBy
{
    [Required]
    [JsonProperty(QuickBaseApiConstants.QuerySortByFieldIdElement)]
    public int FieldId { get; set; }

    [Required]
    [JsonProperty(QuickBaseApiConstants.QuerySortByOrderElement)]
    public string Order { get; set; } = null!;
}