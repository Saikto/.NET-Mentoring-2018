using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using LibrarySystem.ElementWriters.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;

namespace LibrarySystem.ElementWriters
{
    public class NewspaperElementWriter: BaseElementWriter
    {
        public override Type TypeToWrite => typeof(Newspaper);

        public override void WriteElement(XmlWriter xmlWriter, ISystemEntity entity)
        {
            Newspaper newspaper = entity as Newspaper;
            if (newspaper == null)
            {
                throw new ArgumentException($"Provided {nameof(entity)} is null or not type of {nameof(newspaper)}");
            }

            XElement element = new XElement("newspaper");

            AddElement(element, "name", newspaper.Name, true);
            AddElement(element, "note", newspaper.Note);
            AddElement(element, "publicationCity", newspaper.PublicationCity);
            AddElement(element, "publisherName", newspaper.PublisherName);
            AddElement(element, "publicationYear", newspaper.PublicationYear.ToString());
            AddElement(element, "number", newspaper.Number.ToString());
            AddElement(element, "date", newspaper.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
            AddElement(element, "pagesCount", newspaper.PagesCount.ToString());
            AddElement(element, "issn", newspaper.IssnNumber, true);
            
            element.WriteTo(xmlWriter);
        }
    }
}
