using System;
using System.IO;

namespace SiteDownloaderLibrary.Interfaces
{
    public interface ISaver
    {
        void SaveFile(Uri uri, Stream fileStream);
        void SaveHtmlDocument(Uri uri, string name, Stream documentStream);
    }
}
