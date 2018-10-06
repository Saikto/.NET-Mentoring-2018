using FileSystemVisitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitorTest
{
    [TestClass]
    public class FileSystemVisitorTest
    {
        private string _rootPath;
        private FileSystemVisitor.FileSystemVisitor _visitor;
        private List<FileSystemInfo> returnedValues;

        [TestInitialize]
        public void TestInitialize()
        {
            returnedValues = new List<FileSystemInfo>();
            _rootPath = Path.Combine(Directory.GetLogicalDrives().First(), "FileSystemVisitorTest");
            Directory.CreateDirectory(_rootPath);

            for (int i = 1; i <= 3; i++)
            {
                string dirToCreate = Path.Combine(_rootPath, i.ToString());
                Directory.CreateDirectory(dirToCreate);
                string fileToCreate = Path.Combine(_rootPath, i.ToString() + ".txt");
                
                var stream = File.Create(fileToCreate);
                stream.Dispose();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Directory.Delete(_rootPath, true);
        }

        [TestMethod]
        public void CheckFilesVisitor()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(_rootPath);

            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.Count == 6);
        }

        [TestMethod]
        public void CheckFiltration()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(_rootPath, file => file.Name.Contains("2"));

            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.All(a => a.Name.Contains("2") ));
        }

        [TestMethod]
        public void CheckSkipObject()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(_rootPath, file => file.Name.Contains("2"));

            _visitor.FilteredFileFinded += _visitor_FilteredFileFinded;
            
            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.Count == 1);
        }

        [TestMethod]
        public void CheckStopSearch()
        {
            _visitor = new FileSystemVisitor.FileSystemVisitor(_rootPath);

            _visitor.DirectoryFinded += _visitor_DirectoryFinded;

            foreach (var fsObject in _visitor.GetFileSystemTree())
            {
                returnedValues.Add(fsObject);
            }

            Assert.IsTrue(returnedValues.Count != 6);
        }

        private void _visitor_DirectoryFinded(object sender, ItemFoundEventArgs<DirectoryInfo> e)
        {
            e.action = ActionToDo.StopSearch;
        }


        private void _visitor_FilteredFileFinded(object sender, ItemFoundEventArgs<FileInfo> e)
        {
            e.action = ActionToDo.SkipObject;
        }
    }
}
