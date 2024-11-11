using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.CreateRecordModels;

public class RecordMetadata
{
    [JsonProperty(QuickBaseApiConstants.RecordCreatedRecordIdsElement)]
    public List<int>? CreatedRecordIds { get; set; }

    [JsonProperty(QuickBaseApiConstants.RecordTotalNumberOfRecordsProcessed)]
    public int TotalNumberOfRecordsProcessed { get; set; }

    [JsonProperty(QuickBaseApiConstants.RecordUnchangedRecordIds)]
    public List<int>? UnchangedRecordIds { get; set; }

    [JsonProperty(QuickBaseApiConstants.RecordUpdatedRecordIds)]
    public List<int>? UpdatedRecordIds { get; set; }
}