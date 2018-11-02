using System;
using System.Xml.Linq;
using LibrarySystem.ElementParsers.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;

namespace LibrarySystem.ElementParsers
{
    public class NewspaperElementParser: BaseElementParser
    {
        public override string ElementName => "newspaper";

        public override ISystemEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"One of newspaper elements was null.");
            }

            Newspaper newspaper = new Newspaper
            {
                Name = GetElement(element, "name").Value,
                Note = GetElement(element, "note").Value,
                PublicationCity = GetElement(element, "publicationCity").Value,
                PublisherName = GetElement(element, "publisherName").Value,
                PublicationYear = int.Parse(GetElement(element, "publicationYear").Value),
                Number = int.Parse(GetElement(element, "number").Value),
                Date = GetDate(GetElement(element, "date").Value),
                PagesCount = int.Parse(GetElement(element, "pagesCount").Value),
                IssnNumber = GetElement(element, "issn").Value
            };
            return newspaper;
        }
    }
}
