using System.Globalization;
using System.Text.RegularExpressions;
using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class QueryForRecordsRequestModel
{
    [JsonProperty(QuickBaseApiConstants.QueryForRecordsFromElement)]
    public string From { get; set; } = null!;

    [JsonProperty(QuickBaseApiConstants.QueryForRecordsSelectElement)]
    public List<int> Select { get; set; } = null!;

    [JsonProperty(QuickBaseApiConstants.QueryForRecordsWhereElement)]
    public string? Where { get; set; }

    [JsonProperty(QuickBaseApiConstants.QueryForRecordsSortByElement)]
    public List<QueryForDataSortBy>? SortBy { get; set; }

    [JsonProperty(QuickBaseApiConstants.QueryForRecordsOptionsElement)]
    public QueryForDataOptionsModel Options { get; set; } = null!;
}