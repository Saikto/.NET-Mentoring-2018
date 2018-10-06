using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitor
{
    public class FileSystemVisitor: IFileSystemVisitor
    {
        private DirectoryInfo _rootDir;
        private Func<FileSystemInfo, bool> _filter;

        public event EventHandler<StartEventArgs> Start;
        public event EventHandler<FinishEventArgs> Finish;
        public event EventHandler<ItemFoundEventArgs<FileInfo>> FileFinded;
        public event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        public FileSystemVisitor(string rootPath)
        {
            InitRootDir(rootPath);
        }

        public FileSystemVisitor(string rootPath, Func<FileSystemInfo, bool> filter)
        {
            InitRootDir(rootPath);
            _filter = filter;
        }

        private void InitRootDir(string rootPath)
        {
            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(message: "Directory does not exists.");

            _rootDir = new DirectoryInfo(rootPath);
        }

        public IEnumerable<FileSystemInfo> GetFileSystemTree()
        {
            OnEvent(Start, new StartEventArgs());

            ActionToDo currentAction;

            foreach (var fsObject in GetFsObject(_rootDir))
            {
                currentAction = GenerateItemFoundEvent(fsObject, true);

                if (currentAction == ActionToDo.SkipObject)
                {
                    continue;
                }
                if (currentAction == ActionToDo.StopSearch)
                {
                    yield return fsObject;
                    yield break;
                }
                if (currentAction == ActionToDo.Continue)
                {
                    if (_filter != null)
                    {
                        if (_filter(fsObject))
                        {
                            currentAction = GenerateItemFoundEvent(fsObject, false);
                            if (currentAction == ActionToDo.SkipObject)
                            {
                                continue;
                            }
                            if (currentAction == ActionToDo.StopSearch)
                            {
                                yield return fsObject;
                                yield break;
                            }
                            if (currentAction == ActionToDo.Continue)
                                yield return fsObject;
                        }
                    }
                    else
                        yield return fsObject;
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

        public ActionToDo GenerateItemFoundEvent(FileSystemInfo fsObject, bool beforeFilter)
        {
            var fileEventArg = new ItemFoundEventArgs<FileInfo>();
            var directoryEventArg = new ItemFoundEventArgs<DirectoryInfo>();

            if (fsObject is FileInfo)
            {
                if (beforeFilter)
                {
                    OnEvent(FileFinded, fileEventArg);
                }
                else
                {
                    OnEvent(FilteredFileFinded, fileEventArg);
                }
                return fileEventArg.action;
            }
            else
            {
                if (beforeFilter)
                {
                    OnEvent(DirectoryFinded, directoryEventArg);
                }
                else
                {
                    OnEvent(FilteredDirectoryFinded, directoryEventArg);
                }
                return directoryEventArg.action;
            }
        }

        private void OnEvent<T>(EventHandler<T> Event, T args)
        {
            Event?.Invoke(this, args);
        }
    }     
}
