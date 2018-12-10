using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using SiteCopyApp.Utilities;
using SiteDownloaderLibrary;
using SiteDownloaderLibrary.Interfaces;
using SiteDownloaderLibrary.Restrictions;
using FileSaver = SiteCopyApp.Utilities.FileSaver;

namespace SiteCopyApp
{
    class Program
    {
        private static Logger _logger;

        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CmdOptions>(args);

            options.WithParsed(o =>
            {
                if (o.DeepLevel < 0)
                {
                    Console.WriteLine("deepLevel can't be less than zero!");
                    return;
                }

                _logger = new Logger(o.Verbose);
                DirectoryInfo rootDirectory = new DirectoryInfo(o.OutputDirectoryPath);
                FileSaver fSaver = new FileSaver(rootDirectory);
                List<IRestriction> restrictions = GetRestrictionsFromOptions(o);
                SiteDownloader downloader = new SiteDownloader(fSaver, restrictions, o.DeepLevel);
                downloader.LogMessageSent += Downloader_LogMessageSent;

                try
                {
                    downloader.LoadFromUrl(o.Url);
                }
                catch (Exception ex)
                {
                    _logger.Log($"Error during site downloading: {ex.Message}");
                }
            });
        }

        public static List<IRestriction> GetRestrictionsFromOptions(CmdOptions options)
        {
            List<IRestriction> restrictions = new List<IRestriction>();
            if (options.AvailableExtensions != null)
            {
                restrictions.Add(new ExtensionRestriction(options.AvailableExtensions.Split(',').Select(e => "." + e).ToList()));
            }

            restrictions.Add(new DomainRestriction(new Uri(options.Url), options.AllowedDomains));

            return restrictions;
        }

        private static void Downloader_LogMessageSent(string message)
        {
            _logger.Log(message);
        }

    }
}
