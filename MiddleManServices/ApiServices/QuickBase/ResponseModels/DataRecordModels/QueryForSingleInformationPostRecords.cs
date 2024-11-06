using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForSingleInformationPostRecords
{
    [JsonProperty("6")]
    public FieldValue? Thumbnail { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty("7")]
    public FieldValue? Topic { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty("14")]
    public FieldValue? Summary { get; set; } // Field ID 14 represents "Summary"

    [JsonProperty("8")]
    public FieldValue? Category { get; set; }

    [JsonProperty("9")]
    public FieldValue? FirstParagraph { get; set; }

    [JsonProperty("10")]
    public FieldValue? SecondParagraph { get; set; }

    [JsonProperty("11")]
    public FieldValue? SectionImageUrl { get; set; }

    [JsonProperty("16")]
    public FieldValue? HeaderImageUrl { get; set; }

    [JsonProperty("17")]
    public FieldValue? PostViews { get; set; }
}