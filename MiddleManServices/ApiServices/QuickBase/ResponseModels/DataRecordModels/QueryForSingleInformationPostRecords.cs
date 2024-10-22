using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForSingleInformationPostRecords
{
    [JsonProperty("6")]
    public FieldValue? Field6 { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty("7")]
    public FieldValue? Field7 { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty("14")]
    public FieldValue? Field14 { get; set; } // Field ID 14 represents "Summary"

    [JsonProperty("8")]
    public FieldValue? Field8 { get; set; }

    [JsonProperty("9")]
    public FieldValue? Field9 { get; set; }

    [JsonProperty("10")]
    public FieldValue? Field10 { get; set; }

    [JsonProperty("11")]
    public FieldValue? Field11 { get; set; }

    [JsonProperty("16")]
    public FieldValue? Field16 { get; set; }
}