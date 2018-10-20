using System.Configuration;

namespace DirectoryDefender.Configuration.Rule
{
    public class RuleElement: ConfigurationElement
    {
        [ConfigurationProperty("fileNamePattern", IsKey = true)]
        public string Pattern => (string)base["fileNamePattern"];

        [ConfigurationProperty("moveToFolder", IsRequired = true)]
        public string TargetFolder => (string)base["moveToFolder"];

        [ConfigurationProperty("addOrderToName", IsRequired = false, DefaultValue = false)]
        public bool AddOrder => (bool)base["addOrderToName"];

        [ConfigurationProperty("addDateToName", IsRequired = false, DefaultValue = false)]
        public bool AddDate => (bool)base["addDateToName"];
    }
}
