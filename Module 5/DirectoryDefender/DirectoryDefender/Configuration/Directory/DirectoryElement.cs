using System.Configuration;

namespace DirectoryDefender.Configuration.Directory
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("directoryToWatch", IsKey = true, IsRequired = true)]
        public string Directory => (string)base["directoryToWatch"];
    }
}
