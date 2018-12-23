using System.IO;
using System.Text;
using System.Xml;
using BooksLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;

namespace BooksTests
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestMethod()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));

            Catalog catalog;
            using (var fileStream = File.OpenRead("books.xml"))
            {
                catalog = (Catalog)serializer.Deserialize(fileStream);
            }

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, Catalog.XmlNamespace);

            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };

            using (var xmlWriter = XmlWriter.Create("booksAfter.xml", xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, catalog, namespaces);
            }
        }
    }
}
