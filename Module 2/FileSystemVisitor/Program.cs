using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemVisitor;

namespace FileSystemVisitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"E:\Root";

            FileSystemVisitor visitor = new FileSystemVisitor(path, file => file.Name.Contains("3"));

            visitor.Start += Visitor_Start;

            visitor.DirectoryFinded += Visitor_DirectoryFinded;

            foreach (var i in visitor.GetFileSystemTree(ActionToDo.StopSearch))
            {
                Console.WriteLine(i.Name);
            }

            Console.ReadLine();
        }

        private static void Visitor_DirectoryFinded(object sender, ItemFoundEventArgs<DirectoryInfo> e)
        {
            throw new NotImplementedException();
        }

        private static void Visitor_Start(object sender, StartEventArgs e)
        {
            Console.WriteLine("Search started!");
        }
    }
}
