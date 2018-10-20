using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DirectoryDefender
{
    public class FileMover
    {
        private int _movedFilesCounter = 1;
        private readonly DirectoryInfo _defaultMoveToFolder;
        private readonly List<MovingRule> _rules;

        public FileMover(DirectoryInfo defaultMoveToFolder,  List<MovingRule> rules)
        {
            _rules = new List<MovingRule>();
            _defaultMoveToFolder = defaultMoveToFolder;
            _rules = rules;
        }

        public void ProcessFile(FileSystemEventArgs e)
        {
            string initialFileName = e.Name;
            string initialFilePath = e.FullPath;
            string extension = Path.GetExtension(initialFileName);
            string endFileName = Path.GetFileNameWithoutExtension(initialFileName);
            string destinationFileFolder;
            string destinationFilePath;

            foreach (var rule in _rules)
            {
                if (rule.FileNamePattern.IsMatch(initialFileName))
                {
                    Logger.Log(Resources.Messages.RuleFound);

                    destinationFileFolder = rule.DestinationFolder.FullName;

                    if (rule.AddDate)
                    {
                        var formatInfo = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
                        formatInfo.DateSeparator = ".";
                        endFileName = endFileName + "_" + DateTime.Now.ToString(formatInfo.ShortDatePattern);
                    }

                    if (rule.AddOrder)
                    {
                        endFileName = endFileName + "_" + _movedFilesCounter;
                    }
                    
                    endFileName = endFileName + extension;

                    destinationFilePath = Path.Combine(destinationFileFolder, endFileName);

                    if (!Directory.Exists(destinationFileFolder))
                    {
                        Logger.Log(string.Format(Resources.Messages.DestinationDirDidNotExist, destinationFileFolder));
                        Directory.CreateDirectory(destinationFileFolder);
                    }

                    File.Move(initialFilePath, destinationFilePath);

                    Logger.Log(string.Format(Resources.Messages.FileMoved, initialFileName, initialFilePath, destinationFilePath));
                    return;
                }
            }

            Logger.Log(Resources.Messages.RuleNotFound);

            destinationFileFolder = _defaultMoveToFolder.FullName;
            if (!Directory.Exists(destinationFileFolder))
            {
                
                Directory.CreateDirectory(destinationFileFolder);
            }

            endFileName = endFileName + extension;
            destinationFilePath = Path.Combine(destinationFileFolder, endFileName);

            File.Move(initialFilePath, destinationFilePath);

            Logger.Log(string.Format(Resources.Messages.FileMoved, initialFileName, initialFilePath, destinationFilePath));

            _movedFilesCounter++;
        }
    }
}
