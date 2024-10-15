using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.CreateRecordModels;

public class RecordMetadata
{
    [JsonProperty("createdRecordIds")]
    public List<int>? CreatedRecordIds { get; set; }

    [JsonProperty("totalNumberOfRecordsProcessed")]
    public int TotalNumberOfRecordsProcessed { get; set; }

    [JsonProperty("unchangedRecordIds")]
    public List<int>? UnchangedRecordIds { get; set; }

    [JsonProperty("updatedRecordIds")]
    public List<int>? UpdatedRecordIds { get; set; }
}