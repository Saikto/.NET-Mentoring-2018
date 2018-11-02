namespace LibrarySystem.SystemEntities.Creator
{
    public class Inventor: Abstract.Creator
    {
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            Inventor inventor = other as Inventor;
            if (!(inventor is Inventor))
                return false;

            return string.Equals(Name, inventor.Name) && string.Equals(Surname, inventor.Surname);
        }
    }
}
