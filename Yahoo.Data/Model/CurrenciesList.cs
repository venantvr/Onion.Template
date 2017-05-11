using System.Xml.Serialization;
using Yahoo.Data.Interfaces;

namespace Yahoo.Data.Model
{
    [XmlRoot(ElementName = "list")]
    public class CurrenciesList : IDtoObject
    {
        [XmlElement(ElementName = "meta")]
        public Meta Meta { get; set; }

        [XmlElement(ElementName = "resources")]
        public Resources Resources { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}