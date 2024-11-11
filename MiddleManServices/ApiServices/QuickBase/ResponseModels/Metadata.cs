using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class Metadata
{
    [JsonProperty(QuickBaseApiConstants.MetadataNumFieldsElement)]
    public int NumFields { get; set; }

    [JsonProperty(QuickBaseApiConstants.MetadataNumRecordsElement)]
    public int NumRecords { get; set; }

    [JsonProperty(QuickBaseApiConstants.MetadataSkipElement)]
    public int Skip { get; set; }

    [JsonProperty(QuickBaseApiConstants.MetadataTotalRecordsElement)]
    public int TotalRecords { get; set; }
}