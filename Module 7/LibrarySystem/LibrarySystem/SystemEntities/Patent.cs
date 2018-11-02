using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities.Creator;

namespace LibrarySystem.SystemEntities
{
    public class Patent: ISystemEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Country { get; set; }
        public List<Inventor> Inventors { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PagesCount { get; set; }
        public string RegistrationNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Patent patent = obj as Patent;
            if (!(patent is Patent))
                return false;

            return string.Equals(Name, patent.Name) 
                   && string.Equals(Note, patent.Note) 
                   && string.Equals(Country, patent.Country) 
                   && Inventors.SequenceEqual(patent.Inventors) 
                   && ApplicationDate.Equals(patent.ApplicationDate) 
                   && PublicationDate.Equals(patent.PublicationDate) 
                   && PagesCount == patent.PagesCount 
                   && string.Equals(RegistrationNumber, patent.RegistrationNumber);
        }
    }
}
