/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */

using System.Xml.Serialization;

namespace Yahoo.Data.Model
{
    [XmlRoot(ElementName = "meta")]
    public class Meta
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
    }
}