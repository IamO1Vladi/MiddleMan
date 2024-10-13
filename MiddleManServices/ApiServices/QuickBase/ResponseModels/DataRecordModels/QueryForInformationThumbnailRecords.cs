using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForInformationThumbnailRecords
{
    [JsonProperty("6")]
    public FieldValue? Field6 { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty("7")]
    public FieldValue? Field7 { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty("14")]
    public FieldValue? Field14 { get; set; } // Field ID 14 represents "Summary"
}