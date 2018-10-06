using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public interface IFileSystemVisitor
    {
        IEnumerable<FileSystemInfo> GetFileSystemTree();
        ActionToDo GenerateItemFoundEvent(FileSystemInfo fsObject, bool beforeFilter);
        event EventHandler<StartEventArgs> Start;
        event EventHandler<FinishEventArgs> Finish;
        event EventHandler<ItemFoundEventArgs<FileInfo>> FileFinded;
        event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFinded;
        event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFinded;
        event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFinded;
    }
}
