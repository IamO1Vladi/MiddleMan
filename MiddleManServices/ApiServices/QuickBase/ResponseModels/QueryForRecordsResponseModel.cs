using MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels;

public class QueryForRecordsResponseModel
{
    public List<QueryForInformationThumbnailRecords>? Data { get; set; }
    public Metadata Metadata { get; set; } = null!;
}