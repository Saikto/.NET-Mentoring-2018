using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public enum ActionToDo
    {
        Continue = 1,
        StopSearch = 2,
        SkipObject = 3
    }

    public class StartEventArgs : EventArgs
    {
    }

    public class FinishEventArgs : EventArgs
    {
    }

    public class ItemFoundEventArgs<T> : EventArgs where T : FileSystemInfo
    {
    }

    public class FileSystemVisitor
    {
        private DirectoryInfo _rootDir;
        private Func<FileSystemInfo, bool> _filter;

        public event EventHandler<StartEventArgs> Start;
        public event EventHandler<FinishEventArgs> Finish;
        public event EventHandler<ItemFoundEventArgs<FileInfo>> FileFinded;
        public event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        private void OnEvent<T>(EventHandler<T> Event, T args)
        {
            Event?.Invoke(this, args);
        }

        public FileSystemVisitor(string rootPath)
        {
            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(message: "Directory does not exists.");

            _rootDir = new DirectoryInfo(rootPath);
        }

        public FileSystemVisitor(string rootPath, Func<FileSystemInfo, bool> filter)
        {
            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(message: "Directory does not exists.");

            _rootDir = new DirectoryInfo(rootPath);
            _filter = filter;
        }

        public IEnumerable<FileSystemInfo> GetFileSystemTree(ActionToDo action)
        {
            OnEvent(Start, new StartEventArgs());
            if (_filter == null)
            {
                foreach (var fsObject in GetFsObject(_rootDir))
                {
                    if (fsObject is FileSystemInfo)
                        OnEvent(FileFinded, new ItemFoundEventArgs<FileInfo>());
                    else
                        OnEvent(DirectoryFinded, new ItemFoundEventArgs<DirectoryInfo>());
                    yield return fsObject;
                }
            }
            else
            {
                foreach (var fsObject in GetFsObject(_rootDir))
                {
                    if (_filter(fsObject))
                    {
                        if (fsObject is FileSystemInfo)
                            OnEvent(FilteredFileFinded, new ItemFoundEventArgs<FileInfo>());
                        else
                            OnEvent(FilteredDirectoryFinded, new ItemFoundEventArgs<DirectoryInfo>());
                        yield return fsObject;
                    }
                    else
                        continue;
                }
            }
            OnEvent(Finish, new FinishEventArgs());
        }

        private IEnumerable<FileSystemInfo> GetFsObject(DirectoryInfo startDir)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirectories = null;

            try
            {
                files = startDir.GetFiles();
                subDirectories = startDir.GetDirectories();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            if (files == null || subDirectories == null)
                yield break;

            foreach (var file in files)
            {
                yield return file;
            }

            foreach (var directoryInfo in subDirectories)
            {
                yield return directoryInfo;

                foreach (var fsObject in GetFsObject(directoryInfo))
                    yield return fsObject;
            }
        }
    }     
}
