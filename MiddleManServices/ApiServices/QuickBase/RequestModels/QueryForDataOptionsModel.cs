using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForDataOptionsModel
{
    [JsonProperty("skip")]
    public int Skip { get; set; }

    [JsonProperty("top")]
    public int Top { get; set; }

    [JsonProperty("compareWithAppLocalTime")]
    public bool CompareWithAppLocalTime { get; set; }
}