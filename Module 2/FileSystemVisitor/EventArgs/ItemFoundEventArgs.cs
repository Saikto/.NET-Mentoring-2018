using System;
using System.IO;

namespace FileSystemVisitor
{
    public class ItemFoundEventArgs<T> : EventArgs where T : FileSystemInfo
    {
        public ActionToDo action { get; set; }
    }
}
