using System.Collections.Generic;
using System.Xml.Serialization;

namespace Yahoo.Data
{
    [XmlRoot(ElementName = "resources")]
    public class Resources
    {
        [XmlElement(ElementName = "resource")]
        public List<Resource> Resource { get; set; }

        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }

        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }
}