using System.Xml.Linq;

namespace LibrarySystem.Interfaces
{
    public interface IElementParser
    {
        string ElementName { get; }

        ISystemEntity ParseElement(XElement element);
    }
}
