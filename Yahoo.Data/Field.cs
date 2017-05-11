using System.Xml.Serialization;

namespace Yahoo.Data
{
    [XmlRoot(ElementName = "field")]
    public class Field
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}