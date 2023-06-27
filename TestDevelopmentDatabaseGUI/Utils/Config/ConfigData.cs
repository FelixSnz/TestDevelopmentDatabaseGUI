
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestDevelopmentDatabaseGUI.Utils.Config
{
    [XmlRoot("Config")]
    public class ConfigData
    {
        public class Sql
        {

            public Sql()
            {
                AvailableHosts = new List<string>();
                AvailableDatabases = new List<string>();
            }

            [XmlElement("Host")]
            public string SelectedHost { get; set; }

            [XmlElement("Database")]
            public string SelectedDatabase { get; set; }

            [XmlElement("Username")]
            public string SelectedUser { get; set; }

            [XmlElement("Password")]
            public string Password { get; set; }

            [XmlElement("TableName")]
            public string TableName { get; set; }


            [XmlArray("AvailableHosts"), XmlArrayItem("Host", typeof(string))]
            public List<string> AvailableHosts { get; set; } = new List<string>();


            [XmlArray("AvailableDatabases"), XmlArrayItem("Database", typeof(string))]
            public List<string> AvailableDatabases { get; set; } = new List<string>();

            [XmlArray("AvailableUsernames"), XmlArrayItem("Username", typeof(string))]
            public List<string> AvailableUsernames { get; set; } = new List<string>();
        }
    }
}
