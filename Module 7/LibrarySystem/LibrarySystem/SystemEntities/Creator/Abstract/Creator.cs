namespace LibrarySystem.SystemEntities.Creator.Abstract
{
    public abstract class Creator
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            Creator creator = other as Creator;
            if (!(creator is Creator))
                return false;

            return string.Equals(Name, creator.Name) && string.Equals(Surname, creator.Surname);
        }
    }
}
