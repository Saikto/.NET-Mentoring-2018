using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using LibrarySystem.ElementWriters.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;

namespace LibrarySystem.ElementWriters
{
    public class BookElementWriter : BaseElementWriter
    {
        public override Type TypeToWrite => typeof(Book);

        public override void WriteElement(XmlWriter xmlWriter, ISystemEntity entity)
        {
            Book book = entity as Book;
            if (book == null)
            {
                throw new ArgumentException($"Provided {nameof(entity)} is null or not type of {nameof(Book)}");
            }

            XElement element = new XElement("book");

            AddElement(element, "name", book.Name, true);
            AddElement(element, "note", book.Note);
            AddElement(element, "authors", book.Authors?.Select(
                a => new XElement("author",
                    new XAttribute("name", a.Name),
                    new XAttribute("surname", a.Surname))));
            AddElement(element, "publicationCity", book.PublicationCity);
            AddElement(element, "publisherName", book.PublisherName);
            AddElement(element, "publicationYear", book.PublicationYear.ToString());
            AddElement(element, "pagesCount", book.PagesCount.ToString());
            AddElement(element, "isbn", book.IsbnNumber, true);

            element.WriteTo(xmlWriter);
        }
    }
}
