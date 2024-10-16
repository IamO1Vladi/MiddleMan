using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForRecordsRequestModel
{
    [JsonProperty("from")]
    public string From { get; set; } = null!;

    [JsonProperty("select")]
    public List<int> Select { get; set; } = null!;

    [JsonProperty("where")]
    public string? Where { get; set; }

    // Options fields directly within the main request class
    [JsonProperty("skip")]
    public int Skip { get; set; }

    [JsonProperty("top")]
    public int Top { get; set; }

    [JsonProperty("compareWithAppLocalTime")]
    public bool CompareWithAppLocalTime { get; set; }
}