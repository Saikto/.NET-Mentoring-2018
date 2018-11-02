namespace LibrarySystem.SystemEntities.Creator
{
    public class Author: Abstract.Creator
    {
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            Author author = other as Author;
            if (!(author is Author))
                return false;

            return string.Equals(Name, author.Name) && string.Equals(Surname, author.Surname);
        }
    }
}
