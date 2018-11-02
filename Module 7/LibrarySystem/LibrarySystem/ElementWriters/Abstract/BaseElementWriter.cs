using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using LibrarySystem.Interfaces;

namespace LibrarySystem.ElementWriters.Abstract
{
    public abstract class BaseElementWriter: IElementWriter
    {
        public abstract Type TypeToWrite { get; }

        public abstract void WriteElement(XmlWriter xmlWriter, ISystemEntity entity);

        protected string GetInvariantShortDateString(DateTime date)
        {
            return date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture);
        }

        protected void AddElement(XElement element, string elementName, object value, bool isRequired = false)
        {
            if (value == null && isRequired)
            {
                throw new XmlSchemaException($"Obligatory element '{elementName}' is null or empty.");
            }

            if (value is string && isRequired && string.IsNullOrEmpty(value.ToString()))
            {
                throw new XmlSchemaException($"Obligatory element '{elementName}' is null or empty.");
            }

            var newElement = new XElement(elementName, value);
            element.Add(newElement);
        }
    }
}
