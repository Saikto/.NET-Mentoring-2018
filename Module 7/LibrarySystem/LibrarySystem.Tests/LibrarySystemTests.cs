using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using LibrarySystem.Interfaces;
using LibrarySystem.SystemEntities;
using LibrarySystem.SystemEntities.Creator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibrarySystem.Tests
{
    [TestClass]
    public class LibrarySystemTests
    {
        private LibrarySystem _library;

        #region TestEntities
        private readonly Book _book = new Book
        {
            Name = "Book name",
            Note = "Some note",
            Authors = new List<Author>
            {
                new Author{ Name = "authorName", Surname = "authorSurname"},
                new Author{ Name = "authorName1", Surname = "authorSurname1"}

            },
            PublicationCity = "someCity",
            PublisherName = "somePublisherName",
            PublicationYear = 1995,
            PagesCount = 255,
            IsbnNumber = "1-2323-3232-322323232332"
        };

        private readonly Book _bookEmptyName = new Book
        {
            Name = "",
            Note = "Some note",
            Authors = new List<Author>
            {
                new Author{ Name = "authorName", Surname = "authorSurname"},
                new Author{ Name = "authorName1", Surname = "authorSurname1"}

            },
            PublicationCity = "someCity",
            PublisherName = "somePublisherName",
            PublicationYear = 1995,
            PagesCount = 255,
            IsbnNumber = "1-2323-3232-322323232332"
        };

        private readonly Book _bookNullName = new Book
        {
            Note = "Some note",
            Authors = new List<Author>
            {
                new Author{ Name = "authorName", Surname = "authorSurname"},
                new Author{ Name = "authorName1", Surname = "authorSurname1"}

            },
            PublicationCity = "someCity",
            PublisherName = "somePublisherName",
            PublicationYear = 1995,
            PagesCount = 255,
            IsbnNumber = "1-2323-3232-322323232332"
        };

        private readonly Newspaper _newspaper = new Newspaper
        {
            Name = "Newspaper name",
            Note = "Some note",
            PublicationCity = "someCity",
            PublisherName = "somePublisherName",
            PublicationYear = 1995,
            Number = 444,
            Date = new DateTime(2018, 11, 2),
            PagesCount = 255,
            IssnNumber = "1-2323-3232-322323232332"
        };

        private readonly Patent _patent = new Patent
        {
            Name = "Patent name",
            Note = "Some note",
            Country = "someCountry",
            Inventors = new List<Inventor>
            {
                new Inventor{ Name = "inventorName", Surname = "inventorSurname"},
                new Inventor{ Name = "inventorName1", Surname = "inventorSurname1"}

            },
            ApplicationDate = new DateTime(1995, 10, 5),
            PublicationDate = new DateTime(2018, 11, 2),
            PagesCount = 555,
            RegistrationNumber = "someNumber"
        };
        #endregion

        #region TestXmls

        private readonly string _goodXml_All = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                                               "<catalog>" +
                                               "<book>" +
                                               "<name>Book name</name>" +
                                               "<note>Some note</note>" +
                                               "<authors>" +
                                               "<author name=\"authorName\" surname=\"authorSurname\" />" +
                                               "<author name=\"authorName1\" surname=\"authorSurname1\" />" +
                                               "</authors>" +
                                               "<publicationCity>someCity</publicationCity>" +
                                               "<publisherName>somePublisherName</publisherName>" +
                                               "<publicationYear>1995</publicationYear>" +
                                               "<pagesCount>255</pagesCount>" +
                                               "<isbn>1-2323-3232-322323232332</isbn>" +
                                               "</book>" +
                                               "<newspaper>" +
                                               "<name>Newspaper name</name>" +
                                               "<note>Some note</note>" +
                                               "<publicationCity>someCity</publicationCity>" +
                                               "<publisherName>somePublisherName</publisherName>" +
                                               "<publicationYear>1995</publicationYear>" +
                                               "<number>444</number>" +
                                               "<date>11/02/2018</date>" +
                                               "<pagesCount>255</pagesCount>" +
                                               "<issn>1-2323-3232-322323232332</issn>" +
                                               "</newspaper>" +
                                               "<patent>" +
                                               "<name>Patent name</name>" +
                                               "<note>Some note</note>" +
                                               "<country>someCountry</country>" +
                                               "<inventors>" +
                                               "<inventor name=\"inventorName\" surname=\"inventorSurname\" />" +
                                               "<inventor name=\"inventorName1\" surname=\"inventorSurname1\" />" +
                                               "</inventors>" +
                                               "<applicationDate>10/05/1995</applicationDate>" +
                                               "<publicationDate>11/02/2018</publicationDate>" +
                                               "<pagesCount>555</pagesCount>" +
                                               "<registrationNumber>someNumber</registrationNumber>" +
                                               "</patent>" +
                                               "</catalog>";

        private readonly string _badBookXml_NameEmpty = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                                                        "<catalog>" +
                                                        "<book>" +
                                                        "<name></name>" +
                                                        "<note>Some note</note>" +
                                                        "<authors>" +
                                                        "<author name=\"authorName\" surname=\"authorSurname\" />" +
                                                        "<author name=\"authorName1\" surname=\"authorSurname1\" />" +
                                                        "</authors>" +
                                                        "<publicationCity>someCity</publicationCity>" +
                                                        "<publisherName>somePublisherName</publisherName>" +
                                                        "<publicationYear>1995</publicationYear>" +
                                                        "<pagesCount>255</pagesCount>" +
                                                        "<isbn>1-2323-3232-322323232332</isbn>" +
                                                        "</book>" +
                                                        "</catalog>";


        private readonly string _badBookXml_NameMissing = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                                                          "<catalog>" +
                                                          "<book>" +
                                                          "<note>Some note</note>" +
                                                          "<authors>" +
                                                          "<author name=\"authorName\" surname=\"authorSurname\" />" +
                                                          "<author name=\"authorName1\" surname=\"authorSurname1\" />" +
                                                          "</authors>" +
                                                          "<publicationCity>someCity</publicationCity>" +
                                                          "<publisherName>somePublisherName</publisherName>" +
                                                          "<publicationYear>1995</publicationYear>" +
                                                          "<pagesCount>255</pagesCount>" +
                                                          "<isbn>1-2323-3232-322323232332</isbn>" +
                                                          "</book>" +
                                                          "</catalog>";
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _library = new LibrarySystem();
        }

        [TestMethod]
        public void TestWrite_GoodXml()
        {
            StringWriter writer = new StringWriter();
            ISystemEntity[] array =
            {
                _book,
                _newspaper,
                _patent
            }; 

            _library.WriteLibrary(writer, array);
            string result = writer.ToString();
            
            Assert.AreEqual(_goodXml_All, result);
        }

        [TestMethod]
        public void TestRead_GoodXml()
        {
            StringReader reader = new StringReader(_goodXml_All);

            var entities = _library.ReadLibrary(reader).ToArray();

            Book book = (Book)entities[0];
            Newspaper newspaper = (Newspaper)entities[1];
            Patent patent = (Patent)entities[2];

            Assert.AreEqual(book, _book);
            Assert.AreEqual(newspaper, _newspaper);
            Assert.AreEqual(patent, _patent);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaException))]
        public void TestRead_BookNameEmpty()
        {
            StringReader reader = new StringReader(_badBookXml_NameEmpty);

            var entities = _library.ReadLibrary(reader).ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaException))]
        public void TestRead_BookNameMissing()
        {
            StringReader reader = new StringReader(_badBookXml_NameMissing);

            var entities = _library.ReadLibrary(reader).ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaException))]
        public void TestWrite_BookNameEmpty()
        {
            StringWriter writer = new StringWriter();
            ISystemEntity[] array =
            {
                _bookEmptyName,
            };

            _library.WriteLibrary(writer, array);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaException))]
        public void TestWrite_BookNameMissing()
        {
            StringWriter writer = new StringWriter();
            ISystemEntity[] array =
            {
                _bookNullName,
            };

            _library.WriteLibrary(writer, array);
        }
    }
}
