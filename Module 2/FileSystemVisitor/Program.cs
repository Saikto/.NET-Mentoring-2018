using System;
using System.IO;

namespace FileSystemVisitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"E:\Root";

            FileSystemVisitor visitor = new FileSystemVisitor(path, file => file.Name.Contains("3"));

            //visitor.Start += Visitor_Start;

            visitor.FileFinded += Visitor_FileFinded;
            visitor.DirectoryFinded += Visitor_DirectoryFinded;
            visitor.FilteredFileFinded += Visitor_FilteredFileFinded;
            visitor.FilteredDirectoryFinded += Visitor_FilteredDirectoryFinded;

            foreach (var i in visitor.GetFileSystemTree())
            {
                Console.WriteLine(i.Name);
            }

            Console.ReadLine();
        }

        private static void Visitor_FileFinded(object sender, ItemFoundEventArgs<FileInfo> e)
        {
            Console.WriteLine("Visitor_FileFinded");

        }

        private static void Visitor_DirectoryFinded(object sender, ItemFoundEventArgs<DirectoryInfo> e)
        {
            Console.WriteLine("Visitor_DirectoryFinded");
            e.action = ActionToDo.StopSearch;
        }

        private static void Visitor_FilteredFileFinded(object sender, ItemFoundEventArgs<FileInfo> e)
        {
            Console.WriteLine("Visitor_FilteredFileFinded");
        }

        private static void Visitor_FilteredDirectoryFinded(object sender, ItemFoundEventArgs<DirectoryInfo> e)
        {
            Console.WriteLine("Visitor_FilteredDirectoryFinded");
        }
    }
}
