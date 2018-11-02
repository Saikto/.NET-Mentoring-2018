using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.SystemEntities
{
    public class Newspaper: ISystemEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
        public int PublicationYear { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int PagesCount { get; set; }
        public string IssnNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Newspaper newspaper = obj as Newspaper;
            if (!(newspaper is Newspaper))
                return false;

            return string.Equals(Name, newspaper.Name) 
                   && string.Equals(Note, newspaper.Note) 
                   && string.Equals(PublicationCity, newspaper.PublicationCity) 
                   && string.Equals(PublisherName, newspaper.PublisherName) 
                   && PublicationYear == newspaper.PublicationYear 
                   && Number == newspaper.Number 
                   && Date.Equals(newspaper.Date) 
                   && PagesCount == newspaper.PagesCount 
                   && string.Equals(IssnNumber, newspaper.IssnNumber);
        }
    }
}
