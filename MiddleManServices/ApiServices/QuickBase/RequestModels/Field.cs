using System.Xml.Serialization;

namespace MiddleManServices.ApiServices.QuickBase.RequestModels;

public class Field
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("fid")]
    public string? FieldId { get; set; }

    [XmlAttribute("filename")]
    public string? FileName { get; set; }

    [XmlText]
    public string? Value { get; set; }
}