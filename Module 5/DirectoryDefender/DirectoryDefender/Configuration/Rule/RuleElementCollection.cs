using System.Configuration;

namespace DirectoryDefender.Configuration.Rule
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultMoveToFolder", IsRequired = true)]
        public string DefaultDirectory => (string)base["defaultMoveToFolder"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Pattern;
        }
    }
}
