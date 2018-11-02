using System;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Schema;
using LibrarySystem.Interfaces;

namespace LibrarySystem.ElementParsers.Abstract
{
    public abstract class BaseElementParser: IElementParser
    {
        public abstract string ElementName { get; }

        public abstract ISystemEntity ParseElement(XElement element);

        protected string GetAttributeValue(XElement element, string name, bool isRequired = false)
        {
            XAttribute attribute = element.Attribute(name);

            if (string.IsNullOrEmpty(attribute?.Value) && isRequired)
            {
                throw new XmlSchemaException($"Obligatory attribute '{name}' is missing.");
            }

            return attribute?.Value;
        }

        protected XElement GetElement(XElement parentElement, string name, bool isRequired = false)
        {
            XElement element = parentElement.Element(name);

            if (string.IsNullOrEmpty(element?.Value) && isRequired)
            {
                throw new XmlSchemaException($"Obligatory element '{name}' is missing.");
            }

            return element;
        }

        protected DateTime GetDate(string dateInvariant)
        {
            if (string.IsNullOrEmpty(dateInvariant))
            {
                return default(DateTime);
            }

            return DateTime.ParseExact(dateInvariant, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
                CultureInfo.InvariantCulture.DateTimeFormat);
        }
    }
}
