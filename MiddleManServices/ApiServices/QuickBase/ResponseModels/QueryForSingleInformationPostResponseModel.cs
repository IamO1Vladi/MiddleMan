using MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class QueryForSingleInformationPostResponseModel
{
    public List<QueryForSingleInformationPostRecords>? Data { get; set; }
    public Metadata Metadata { get; set; } = null!;

}