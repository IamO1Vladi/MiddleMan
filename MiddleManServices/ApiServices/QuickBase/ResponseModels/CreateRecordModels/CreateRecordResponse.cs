using System.ComponentModel.DataAnnotations;
using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.CreateRecordModels;

public class CreateRecordResponse
{
    [Required]
    [JsonProperty(QuickBaseApiConstants.CreateRecordResponseMetadataElement)]
    public RecordMetadata Metadata { get; set; } = null!;
}