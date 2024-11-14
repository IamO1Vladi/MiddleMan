using System.Text;
using System.Xml.Serialization;

namespace MiddleMan.Common.Utilities;

public class XmlHelper
{
    public T Deserialize<T>(string xmlInput)
    {
        XmlRootAttribute xmlRoot = new XmlRootAttribute(GetRootElementNameFromXml(xmlInput));
        XmlSerializer serializer = new XmlSerializer(typeof(T), xmlRoot);

        using StringReader reader = new StringReader(xmlInput);

        T usersDtos = (T)serializer.Deserialize(reader);

        return usersDtos;
    }

    public T[] DeserializeCollection<T>(string xmlInput)
    {
        XmlRootAttribute xmlRoot = new XmlRootAttribute(GetRootElementNameFromXml(xmlInput));
        XmlSerializer serializer = new XmlSerializer(typeof(T[]), xmlRoot);

        using StringReader reader = new StringReader(xmlInput);

        T[] usersDtos = (T[])serializer.Deserialize(reader);

        return usersDtos;
    }

    public string Serialize<T>(T obj, string rootName)
    {

        StringBuilder sb = new StringBuilder();

        XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();


        namespaces.Add(string.Empty, "http://www.sitemaps.org/schemas/sitemap/0.9");

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
        using StringWriter writer = new StringWriter(sb);

        xmlSerializer.Serialize(writer, obj, namespaces);

        return sb.ToString().TrimEnd();
    }


    private string GetRootElementNameFromXml(string inputXml)
    {

        int firstBracketIndex = inputXml.IndexOf("<");
        int startIndex = inputXml.IndexOf("<", firstBracketIndex + 1) + 1;

        int firstClosingBracketIndex = inputXml.IndexOf(">");

        int endIndex = inputXml.IndexOf(">", firstClosingBracketIndex + 1);

        string rootElement = inputXml.Substring(startIndex, endIndex - startIndex);

        return rootElement;

    }
}