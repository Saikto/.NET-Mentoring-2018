using System.IO;
using System.Text.RegularExpressions;

namespace DirectoryDefender
{
    public class MovingRule
    {
        public Regex FileNamePattern;
        public DirectoryInfo DestinationFolder;
        public bool AddOrder;
        public bool AddDate;

        public MovingRule(string fileNamePattern, string destinationFolder, bool addOrder, bool addDate)
        {
            FileNamePattern = new Regex(fileNamePattern);
            DestinationFolder = new DirectoryInfo(destinationFolder);
            AddOrder = addOrder;
            AddDate = addDate;
        }
    }
}
