using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using TestDevelopmentDatabaseGUI.Services;
using TestDevelopmentDatabaseGUI.Utils.Config;

public class GlobalConfig
{
    private static GlobalConfig _instance;
    private static readonly object _syncRoot = new object();
    private const string _configPath = "Config.xml";

    public ConfigData.Sql sqlConfig { get; set;} = new ConfigData.Sql();

    private GlobalConfig()
    {

    }

    public static GlobalConfig Instance
    {
        get
        {
            lock (_syncRoot)
            {
                if (_instance == null)
                {
                    try
                    {
                        if (File.Exists(_configPath))
                        {
                            using (var stream = new FileStream(_configPath, FileMode.Open))
                            {
                                var formatter = new XmlSerializer(typeof(GlobalConfig));
                                _instance = (GlobalConfig)formatter.Deserialize(stream);
                            }
                        }
                        else
                        {
                            _instance = new GlobalConfig();
                        }

                    }
                    catch
                    {
                        _instance = new GlobalConfig();
                    }
                    
                }
                return _instance;
            }
        }
    }

    public void Save()
    {
        lock (_syncRoot)
        {
            using (var stream = new FileStream(_configPath, FileMode.Create))
            {
                var formatter = new XmlSerializer(typeof(GlobalConfig));
                formatter.Serialize(stream, this);
            }
        }
    }
}