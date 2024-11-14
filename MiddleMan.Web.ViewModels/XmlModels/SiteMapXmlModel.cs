using System.Xml.Serialization;

namespace MiddleMan.Web.ViewModels.XmlModels;

[XmlType("urlset")]
public class SiteMapXmlModel
{
    public SiteMapXmlModel()
    {
        this.Urls = new List<SiteMapUrlXmlModel>();
    }

    [XmlAttribute("xmlns")]
    public string Xmlns { get; set; }

    [XmlElement("url")]
    public List<SiteMapUrlXmlModel> Urls { get; set; }
}