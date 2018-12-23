using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BooksLibrary
{
    [XmlRoot(ElementName = "catalog", Namespace = XmlNamespace)]
    public class Catalog
    {
        [XmlAttribute(AttributeName = "date", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement("book")]
        public List<Book> Books { get; set; }

        public const string XmlNamespace = "http://library.by/catalog";
    }
}
