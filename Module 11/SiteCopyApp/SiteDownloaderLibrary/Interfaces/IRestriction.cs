using System;
using SiteDownloaderLibrary.Enums;

namespace SiteDownloaderLibrary.Interfaces
{
    public interface IRestriction
    {
        RestrictionType RestrictionType { get; }
        bool IsAcceptable(Uri uri);
    }
}
