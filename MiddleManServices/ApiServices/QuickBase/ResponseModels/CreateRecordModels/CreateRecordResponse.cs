using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.CreateRecordModels;

public class CreateRecordResponse
{
    [Required]
    [JsonProperty("metadata")]
    public RecordMetadata Metadata { get; set; } = null!;
}