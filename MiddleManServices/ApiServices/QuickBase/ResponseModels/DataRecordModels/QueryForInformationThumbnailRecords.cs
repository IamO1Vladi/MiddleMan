using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForInformationThumbnailRecords
{
    [JsonProperty("6")]
    public FieldValue? ThumbnailUrl { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty("7")]
    public FieldValue? Topic { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty("14")]
    public FieldValue? Summary { get; set; } // Field ID 14 represents "Summary"

    [JsonProperty("3")]
    public FieldValue? RecordId { get; set; }
}