using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using LibrarySystem.ElementWriters.Abstract;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;

namespace LibrarySystem.ElementWriters
{
    public class PatentElementWriter: BaseElementWriter
    {
        public override Type TypeToWrite => typeof(Patent);

        public override void WriteElement(XmlWriter xmlWriter, ISystemEntity entity)
        {
            Patent patent = entity as Patent;
            if (patent == null)
            {
                throw new ArgumentException($"Provided {nameof(entity)} is null or not type of {nameof(Patent)}");
            }

            XElement element = new XElement("patent");

            AddElement(element, "name", patent.Name, true);
            AddElement(element, "note", patent.Note);
            AddElement(element, "country", patent.Country);
            AddElement(element, "inventors", patent.Inventors.Select(
                c => new XElement("inventor",
                    new XAttribute("name", c.Name),
                    new XAttribute("surname", c.Surname))));
            AddElement(element, "applicationDate", GetInvariantShortDateString(patent.ApplicationDate.Date));
            AddElement(element, "publicationDate", GetInvariantShortDateString(patent.PublicationDate.Date));
            AddElement(element, "pagesCount", patent.PagesCount.ToString());
            AddElement(element, "registrationNumber", patent.RegistrationNumber, true);

            element.WriteTo(xmlWriter);
        }
    }
}
