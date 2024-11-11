using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForInformationThumbnailRecords
{
    [JsonProperty(QuickBaseApiConstants.ThumbnailUrlFieldId)]
    public FieldValue? ThumbnailUrl { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty(QuickBaseApiConstants.TopicFieldId)]
    public FieldValue? Topic { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty(QuickBaseApiConstants.SummaryFieldId)]
    public FieldValue? Summary { get; set; } // Field ID 14 represents "Summary"

    [JsonProperty(QuickBaseApiConstants.RecordIdFieldId)]
    public FieldValue? RecordId { get; set; }
}