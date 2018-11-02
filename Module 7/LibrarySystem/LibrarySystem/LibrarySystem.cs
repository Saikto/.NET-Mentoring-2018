using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Reflection;
using LibrarySystem.ElementParsers.Abstract;
using LibrarySystem.ElementWriters.Abstract;
using LibrarySystem.Interfaces;

namespace LibrarySystem
{
    public class LibrarySystem
    {
        private const string CatalogElementName = "catalog";
        private readonly Dictionary<string, IElementParser> _parsersDictionary;
        private readonly Dictionary<Type, IElementWriter> _writersDictionary;

        public LibrarySystem()
        {
            _parsersDictionary = new Dictionary<string, IElementParser>();
            _writersDictionary = new Dictionary<Type, IElementWriter>();
            SetUpParsers();
        }

        private void SetUpParsers()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var parsersTypes = assembly
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseElementParser));

            var writersTypes = assembly
                .GetTypes()
                .Where(t => t.BaseType == typeof(BaseElementWriter));

            foreach (var parserType in parsersTypes)
            {
                var instance = (IElementParser)Activator.CreateInstance(parserType);

                _parsersDictionary.Add(instance.ElementName, instance);
            }

            foreach (var writerType in writersTypes)
            {
                var instance = (IElementWriter)Activator.CreateInstance(writerType);

                _writersDictionary.Add(instance.TypeToWrite, instance);
            }
        }

        public IEnumerable<ISystemEntity> ReadLibrary(TextReader inputReader)
        {
            using (XmlReader xmlReader = XmlReader.Create(inputReader, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
            {
                xmlReader.ReadToFollowing(CatalogElementName);
                xmlReader.ReadStartElement();

                do
                {
                    while (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var elementContent = XNode.ReadFrom(xmlReader) as XElement;

                        if (_parsersDictionary.TryGetValue(elementContent.Name.LocalName, out IElementParser parser))
                        {
                            yield return parser.ParseElement(elementContent);
                        }
                        else
                        {
                            throw new XmlSchemaException($"Found unknown element '{elementContent.Name.LocalName}'.");
                        }
                    }
                } while (xmlReader.Read());
            }
        }

        public void WriteLibrary(TextWriter outputWriter, IEnumerable<ISystemEntity> libraryEntities)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(outputWriter, new XmlWriterSettings()))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(CatalogElementName);
                foreach (var libraryEntity in libraryEntities)
                {
                    if (_writersDictionary.TryGetValue(libraryEntity.GetType(), out IElementWriter writer))
                    {
                        writer.WriteElement(xmlWriter, libraryEntity);
                    }
                    else
                    {
                        throw new XmlSchemaException($"Cannot find writer for element type {libraryEntity.GetType().FullName}");
                    }
                }
                xmlWriter.WriteEndElement();
            }
        }
    }
}
