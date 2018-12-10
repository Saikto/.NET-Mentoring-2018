using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;
using SiteDownloaderLibrary.Enums;
using SiteDownloaderLibrary.Interfaces;

namespace SiteDownloaderLibrary
{
    public class SiteDownloader
    {
        private readonly ISaver _saver;
        private readonly List<IRestriction> _restrictions;
        private const string HtmlDocumentMediaType = "text/html";
        private readonly HashSet<Uri> _visitedUrls = new HashSet<Uri>();

        public int MaxDeepLevel { get; set; }

        public delegate void LogMessage(string message);
        public event LogMessage LogMessageSent;

        public SiteDownloader(ISaver saver, List<IRestriction> restrictions, int maxDeepLevel)
        {
            _saver = saver;
            _restrictions = restrictions;
            MaxDeepLevel = maxDeepLevel;
        }

        public void LoadFromUrl(string url)
        {
            _visitedUrls.Clear();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                ScanUrl(httpClient, httpClient.BaseAddress, 0);
            }
        }

        private void ScanUrl(HttpClient httpClient, Uri uri, int level)
        {
            if (level > MaxDeepLevel || _visitedUrls.Contains(uri) || !IsValidScheme(uri.Scheme))
            {
                return;
            }
            _visitedUrls.Add(uri);

            HttpResponseMessage response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri)).Result;

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.Content.Headers.ContentType.MediaType == HtmlDocumentMediaType)
            {
                ProcessHtmlDocument(httpClient, uri, level);
            }
            else
            {
                ProcessFile(httpClient, uri);
            }
        }
        private void ProcessFile(HttpClient httpClient, Uri uri)
        {
            LogMessageSent?.Invoke($"File founded: {uri}");

            if (!IsAcceptableUri(uri, RestrictionType.File) && !IsAcceptableUri(uri, RestrictionType.Url))
            {
                return;
            }

            var response = httpClient.GetAsync(uri).Result;
            LogMessageSent?.Invoke($"File loaded: {uri}");
            _saver.SaveFile(uri, response.Content.ReadAsStreamAsync().Result);
        }

        private void ProcessHtmlDocument(HttpClient httpClient, Uri uri, int level)
        {
            LogMessageSent?.Invoke($"Url founded: {uri}");
            if (!IsAcceptableUri(uri, RestrictionType.Url))
            {
                return;
            }

            var response = httpClient.GetAsync(uri).Result;
            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);
            LogMessageSent?.Invoke($"Html loaded: {uri}");
            _saver.SaveHtmlDocument(uri, GetDocumentFileName(document), GetDocumentStream(document));

            var attributesWithLinks = document.DocumentNode.Descendants().SelectMany(d => d.Attributes.Where(IsAttributeWithLink));
            foreach (var attributesWithLink in attributesWithLinks)
            {
                ScanUrl(httpClient, new Uri(httpClient.BaseAddress, attributesWithLink.Value), level + 1);
            }
        }

        private string GetDocumentFileName(HtmlDocument document)
        {
            return document.DocumentNode.Descendants("title").FirstOrDefault()?.InnerText + ".html";
        }

        private Stream GetDocumentStream(HtmlDocument document)
        {
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        private bool IsValidScheme(string scheme)
        {
            switch (scheme)
            {
                case "http":
                case "https":
                    return true;
                default:
                    return false;
            }
        }

        private bool IsAttributeWithLink(HtmlAttribute attribute)
        {
            return attribute.Name == "src" || attribute.Name == "href";
        }

        private bool IsAcceptableUri(Uri uri, RestrictionType restrictionType)
        {
            bool result = true;
            foreach (var restriction in _restrictions)
            {
                if (restriction.RestrictionType == restrictionType)
                {
                    result = result && restriction.IsAcceptable(uri);
                }
            }

            return result;
        }
    }
}