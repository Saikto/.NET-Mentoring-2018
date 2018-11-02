using System;
using System.Linq;
using System.Xml.Linq;
using LibrarySystem.ElementParsers.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;
using LibrarySystem.SystemEntities.Creator;

namespace LibrarySystem.ElementParsers
{
    public class BookElementParser: BaseElementParser
    {
        public override string ElementName => "book";

        public override ISystemEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"One of book elements was null.");
            }

            Book book = new Book
            {
                Name = GetElement(element, "name", true).Value,
                Note = GetElement(element, "note").Value,
                Authors = GetElement(element, "authors").Elements("author").Select(a => new Author
                {
                    Name = GetAttributeValue(a, "name"),
                    Surname = GetAttributeValue(a, "surname")
                }).ToList(),
                PublicationCity = GetElement(element, "publicationCity").Value,
                PublisherName = GetElement(element, "publisherName").Value,
                PublicationYear = int.Parse(GetElement(element, "publicationYear").Value),
                PagesCount = int.Parse(GetElement(element, "pagesCount").Value),
                IsbnNumber = GetElement(element, "isbn", true).Value
            };
            return book;
        }
    }
}
