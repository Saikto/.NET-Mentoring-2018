using System;
using System.Linq;
using System.Xml.Linq;
using LibrarySystem.ElementParsers.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;
using LibrarySystem.SystemEntities.Creator;

namespace LibrarySystem.ElementParsers
{
    public class PatentElementParser: BaseElementParser
    {
        public override string ElementName => "patent";

        public override ISystemEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"One of patent elements was null.");
            }

            Patent patent = new Patent
            {
                Name = GetElement(element, "name").Value,
                Note = GetElement(element, "note").Value,
                Country = GetElement(element, "country").Value,
                Inventors = GetElement(element, "inventors").Elements("inventor").Select(
                    a => new Inventor
                    {
                        Name = GetAttributeValue(a, "name"),
                        Surname = GetAttributeValue(a, "surname")
                    }).ToList(),
                ApplicationDate = GetDate(GetElement(element, "applicationDate").Value),
                PublicationDate = GetDate(GetElement(element, "publicationDate").Value),
                PagesCount = int.Parse(GetElement(element, "pagesCount").Value),
                RegistrationNumber = GetElement(element, "registrationNumber").Value
            };
            return patent;
        }
    }
}
