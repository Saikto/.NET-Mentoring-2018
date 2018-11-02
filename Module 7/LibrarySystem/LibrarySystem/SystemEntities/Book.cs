using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities.Creator;

namespace LibrarySystem.SystemEntities
{
    public class Book: ISystemEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public List<Author> Authors { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
        public int PublicationYear { get; set; }
        public int PagesCount { get; set; }
        public string IsbnNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Book book = obj as Book;
            if (!(book is Book))
                return false;

            return string.Equals(Name, book.Name)
                   && string.Equals(Note, book.Note)
                   && Authors.SequenceEqual(book.Authors)
                   && string.Equals(PublicationCity, book.PublicationCity)
                   && string.Equals(PublisherName, book.PublisherName)
                   && PublicationYear == book.PublicationYear
                   && PagesCount == book.PagesCount
                   && string.Equals(IsbnNumber, book.IsbnNumber);
        }
    }
}
