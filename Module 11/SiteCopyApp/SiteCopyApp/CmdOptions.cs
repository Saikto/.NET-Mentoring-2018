using CommandLine;
using SiteDownloaderLibrary.Enums;

namespace SiteCopyApp
{
    public class CmdOptions
    {
        [Option('d', "allowedDomains", Default = 1, HelpText = "Set restrictions for moving to domains (1 - All, 2 - Current domain, 3 - Descendant urls only.")]
        public AllowedDomains AllowedDomains { get; set; }

        [Option('e', "allowedExtensions", Default = null, HelpText = "List of extensions, example: \"pdf,css,js\".")]
        public string AvailableExtensions { get; set; }

        [Option('l', "deepLevel", Default = 2, HelpText = "Max deep level of links to process.")]
        public int DeepLevel { get; set; }

        [Option('v', "verbose", Default = false, HelpText = "Prints processing status into console.")]
        public bool Verbose { get; set; }

        [Option('u', "url", Required = true, HelpText = "Root URL for downloading.")]
        public string Url { get; set; }

        [Option('o', "directory", Required = true, HelpText = "Output directory path.")]
        public string OutputDirectoryPath { get; set; }
    }
}
