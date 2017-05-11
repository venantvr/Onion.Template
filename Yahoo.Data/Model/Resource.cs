using System.Collections.Generic;
using System.Xml.Serialization;

namespace Yahoo.Data.Model
{
    [XmlRoot(ElementName = "resource")]
    public class Resource
    {
        [XmlElement(ElementName = "field")]
        public List<Field> Field { get; set; }

        [XmlAttribute(AttributeName = "classname")]
        public string Classname { get; set; }
    }
}