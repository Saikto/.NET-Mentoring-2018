using System.Configuration;
using System.Globalization;
using DirectoryDefender.Configuration.Directory;
using DirectoryDefender.Configuration.Rule;

namespace DirectoryDefender.Configuration
{
    public class MoverSettingsConfigurationSection: ConfigurationSection
    {
        [ConfigurationProperty("culture", IsRequired = false, DefaultValue = "en-EN")]
        public CultureInfo TargetCulture => (CultureInfo)base["culture"];

        [ConfigurationCollection(typeof(DirectoryElement))]
        [ConfigurationProperty("directories", IsRequired = true)]
        public DirectoryElementCollection DirectoriesToWatch => (DirectoryElementCollection)this["directories"];

        [ConfigurationCollection(typeof(RuleElement))]
        [ConfigurationProperty("rules", IsRequired = true)]
        public RuleElementCollection MovingRules => (RuleElementCollection)this["rules"];
    }
}
