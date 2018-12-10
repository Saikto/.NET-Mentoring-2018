using System;

namespace SiteCopyApp.Utilities
{
    public class Logger
    {
        private readonly bool _enabled;

        public Logger(bool logEnabled)
        {
            _enabled = logEnabled;
        }

        public void Log(string message)
        {
            if (_enabled)
            {
                Console.WriteLine(message);
            }
        }
    }
}
