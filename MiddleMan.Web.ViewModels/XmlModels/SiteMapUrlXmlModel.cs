using System.Xml.Serialization;

namespace MiddleMan.Web.ViewModels.XmlModels;

[XmlType("url")]
public class SiteMapUrlXmlModel
{
    [XmlElement("loc")]
    public string UrlLocation { get; set; }

    [XmlElement("lastmod")]
    public string LastModified { get; set; }

    [XmlElement("changefreq")]
    public string ChangeFrequency { get; set; }

    [XmlElement("priority")]
    public string Priority { get; set; }
}