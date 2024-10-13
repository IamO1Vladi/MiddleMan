using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class Metadata
{
    [JsonProperty("numFields")]
    public int NumFields { get; set; }

    [JsonProperty("numRecords")]
    public int NumRecords { get; set; }

    [JsonProperty("skip")]
    public int Skip { get; set; }

    [JsonProperty("totalRecords")]
    public int TotalRecords { get; set; }
}