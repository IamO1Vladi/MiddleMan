using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using MiddleMan.Common.Constants;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

[XmlType(QuickBaseApiConstants.UploadAFileXmlType)]
public class UploadAFileRequestModel
{
    [Required]
    [XmlElement(QuickBaseApiConstants.UploadAFileUserTokenElement)]
    public string UserToken { get; set; } = null!;

    [Required]
    [XmlElement(QuickBaseApiConstants.UploadAFileRecordIdElement)]
    public string RecordId { get; set; } = null!;

    [XmlElement(QuickBaseApiConstants.UploadAFileFieldElement)]
    public Field Field { get; set; } = null!;
}