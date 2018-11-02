using System;
using System.Xml;

namespace LibrarySystem.Interfaces
{
    public interface IElementWriter
    {
        Type TypeToWrite { get; }

        void WriteElement(XmlWriter xmlWriter, ISystemEntity entity);
    }
}
