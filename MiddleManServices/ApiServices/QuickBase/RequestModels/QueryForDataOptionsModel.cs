using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForDataOptionsModel
{
    [JsonProperty(QuickBaseApiConstants.QueryOptionsSkipElement)]
    public int Skip { get; set; }

    [JsonProperty(QuickBaseApiConstants.QueryOptionsTopElement)]
    public int Top { get; set; }

    [JsonProperty(QuickBaseApiConstants.QueryOptionsCompareWithAppLocalTimeElement)]
    public bool CompareWithAppLocalTime { get; set; }
}