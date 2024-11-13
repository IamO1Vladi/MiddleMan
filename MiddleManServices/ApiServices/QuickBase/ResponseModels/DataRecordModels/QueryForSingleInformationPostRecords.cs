using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class QueryForSingleInformationPostRecords
{
    [JsonProperty(QuickBaseApiConstants.ThumbnailUrlFieldId)]
    public FieldValue? Thumbnail { get; set; }  // Field ID 6 represents "Thumbnail"

    [JsonProperty(QuickBaseApiConstants.TopicFieldId)]
    public FieldValue? Topic { get; set; }  // Field ID 7 represents "Topic"

    [JsonProperty(QuickBaseApiConstants.SummaryFieldId)]
    public FieldValue? Summary { get; set; } // Field ID 14 represents "Summary"

    [JsonProperty(QuickBaseApiConstants.CategoryFieldId)]
    public FieldValue? Category { get; set; }

    [JsonProperty(QuickBaseApiConstants.FirstParagraphFieldId)]
    public FieldValue? FirstParagraph { get; set; }

    [JsonProperty(QuickBaseApiConstants.SecondParagraphFieldId)]
    public FieldValue? SecondParagraph { get; set; }

    [JsonProperty(QuickBaseApiConstants.SectionImageUrlFieldId)]
    public FieldValue? SectionImageUrl { get; set; }

    [JsonProperty(QuickBaseApiConstants.HeaderImageUrlFieldId)]
    public FieldValue? HeaderImageUrl { get; set; }

    [JsonProperty(QuickBaseApiConstants.PostViewFieldId)]
    public FieldValue? PostViews { get; set; }

    [JsonProperty(QuickBaseApiConstants.KeyWordsMetaTag)]
    public FieldValue? KeyWordsMetaTag { get; set; }
}