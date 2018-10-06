using FileSystemVisitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitorTest
{
    [TestClass]
    public class FileSystemVisitorTest
    {
        //private IFileSystemVisitor _visitor;
        //private Mock<FileInfo> _fileInfoMock;
        //private Mock<DirectoryInfo> _directoryInfoMock;


        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _visitor = new FileSystemVisitor.FileSystemVisitor("???", file => file.Name.Contains("2"));
        //    _fileInfoMock = new Mock<FileInfo>();
        //    _directoryInfoMock = new Mock<DirectoryInfo>();
        //}

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    FileInfo fsObject = _fileInfoMock.Object;

        //    _visitor.FileFinded += _visitor_FileFinded;
        //    _visitor.GenerateItemFoundEvent(fsObject, true);


        //}

        //private void _visitor_FileFinded(object sender, ItemFoundEventArgs<FileInfo> e)
        //{
        //    throw new System.NotImplementedException();
        //}

        private FileSystemVisitor.FileSystemVisitor _visitor;
        private List<FileSystemInfo> returnedValues;

        [TestInitialize]
        public void TestInitialize()
        {
            returnedValues = new List<FileSystemInfo>();
        }

        [TestMethod]
        public void CheckFilteredObjectExists()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(@"E:\Root", file => file.Name.Contains("2"));
            _visitor.FileFinded += _visitor_FileFinded;

            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.All(a => a.Name.Contains("2") ));
        }

        [TestMethod]
        public void CheckFilteredObjectNotExist()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(@"E:\Root", file => file.Name.Contains("2"));
            _visitor.FileFinded += _visitor_FileFinded;

            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.All(a => a.Name.Contains("2")));
        }

        private void _visitor_FileFinded(object sender, ItemFoundEventArgs<FileInfo> e)
        {
            throw new System.NotImplementedException();
        }
    }
}
