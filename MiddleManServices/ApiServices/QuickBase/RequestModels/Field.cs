using System.Xml.Serialization;
using MiddleMan.Common.Constants;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class Field
{
    [XmlAttribute(QuickBaseApiConstants.FieldNameElement)]
    public string? Name { get; set; }

    [XmlAttribute(QuickBaseApiConstants.FieldIdElement)]
    public string? FieldId { get; set; }

    [XmlAttribute(QuickBaseApiConstants.FieldFileNameElement)]
    public string? FileName { get; set; }

    [XmlText]
    public string? Value { get; set; }
}