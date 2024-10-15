using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

[XmlType("qbapi")]
public class UploadAFileRequestModel
{
    [Required]
    [XmlElement("usertoken")]
    public string UserToken { get; set; } = null!;

    [Required]
    [XmlElement("rid")]
    public string RecordId { get; set; } = null!;

    [XmlElement("field")]
    public Field Field { get; set; } = null!;
}