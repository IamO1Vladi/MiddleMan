using MiddleMan.Common.Constants;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase.ResponseModels.DataRecordModels;

public class SiteMapDatModel
{
    [JsonProperty(QuickBaseApiConstants.SiteMapFieldId)]
    public FieldValue? SiteMap { get; set; }

}