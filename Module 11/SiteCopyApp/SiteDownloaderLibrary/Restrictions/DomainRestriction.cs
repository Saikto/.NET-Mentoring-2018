using System;
using SiteDownloaderLibrary.Enums;
using SiteDownloaderLibrary.Interfaces;

namespace SiteDownloaderLibrary.Restrictions
{
    public class DomainRestriction: IRestriction
    {
        public RestrictionType RestrictionType => RestrictionType.Url;
        private readonly Uri _rootUri;
        private readonly AllowedDomains _allowedDomains;

        public DomainRestriction(Uri rootUri, AllowedDomains allowedDomains)
        {
            _rootUri = rootUri;
            _allowedDomains = allowedDomains;
        }

        public bool IsAcceptable(Uri uri)
        {
            switch (_allowedDomains)
            {
                case AllowedDomains.All:
                    return true;
                case AllowedDomains.CurrentOnly:
                    if (_rootUri.DnsSafeHost == uri.DnsSafeHost)
                    {
                        return true;
                    }
                    break;
                case AllowedDomains.DescendantOnly:
                    if (_rootUri.IsBaseOf(uri))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
