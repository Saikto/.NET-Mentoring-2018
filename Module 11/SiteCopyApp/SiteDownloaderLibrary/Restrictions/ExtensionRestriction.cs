using System;
using System.Collections.Generic;
using System.Linq;
using SiteDownloaderLibrary.Enums;
using SiteDownloaderLibrary.Interfaces;

namespace SiteDownloaderLibrary.Restrictions
{
    public class ExtensionRestriction: IRestriction
    {
        public RestrictionType RestrictionType => RestrictionType.File;
        private readonly List<string> _allowedExtensions;

        public ExtensionRestriction(List<string> allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        public bool IsAcceptable(Uri uri)
        {
            string lastSegment = uri.Segments.Last();
            return _allowedExtensions.Any(e => lastSegment.EndsWith(e));
        }
    }
}
