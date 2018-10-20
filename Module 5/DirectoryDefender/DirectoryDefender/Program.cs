using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using DirectoryDefender.Configuration;
using DirectoryDefender.Configuration.Directory;
using DirectoryDefender.Configuration.Rule;

namespace DirectoryDefender
{
    public class Program
    {
        private static DirectoryInfo _defaultMoveToFolder;
        private static readonly List<MovingRule> Rules = new List<MovingRule>();
        private static readonly List<FileSystemWatcher> WatchersList = new List<FileSystemWatcher>();
        private static readonly List<DirectoryInfo> DirectoriesToWatch = new List<DirectoryInfo>();
        private static FileMover _mover;

        static void Main()
        {
            SetUpFromAppConfig();
            SetUpWatchers();

            _mover = new FileMover(_defaultMoveToFolder, Rules);

            Console.CancelKeyPress += (sender, e) =>
            {
                Logger.Log(Resources.Messages.ProgramExit);
                Environment.Exit(0);
            };
            while(true) Console.ReadKey(false);
        }

        private static void SetUpFromAppConfig()
        {
            var moverConfigSection = (MoverSettingsConfigurationSection)ConfigurationManager.GetSection("MoverSettings");

            SetAppCulture(moverConfigSection.TargetCulture);

            foreach (DirectoryElement directoryElement in moverConfigSection.DirectoriesToWatch)
            {
                DirectoryInfo directory = new DirectoryInfo(directoryElement.Directory);
                DirectoriesToWatch.Add(directory);
            }

            _defaultMoveToFolder = new DirectoryInfo(moverConfigSection.MovingRules.DefaultDirectory);

            foreach (RuleElement ruleElement in moverConfigSection.MovingRules)
            {
                MovingRule rule = new MovingRule(ruleElement.Pattern, ruleElement.TargetFolder, ruleElement.AddOrder, ruleElement.AddDate);
                Rules.Add(rule);
            }
        }

        private static void SetUpWatchers()
        {
            foreach (var directory in DirectoriesToWatch)
            {
                if (directory.Exists)
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(directory.FullName)
                    {
                        NotifyFilter = NotifyFilters.FileName, EnableRaisingEvents = true
                    };
                    watcher.Created += OnCreate;
                    WatchersList.Add(watcher);
                }
                else
                {
                    Logger.Log(string.Format(Resources.Messages.DirToWatchNotExists, directory.FullName));
                }
            }
        }

        private static void OnCreate(object sender, FileSystemEventArgs e)
        {
            Logger.Log(Environment.NewLine);
            Logger.Log(string.Format(Resources.Messages.FileFound, e.Name, DateTime.Now));
            _mover.ProcessFile(e);
        }

        private static void SetAppCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
