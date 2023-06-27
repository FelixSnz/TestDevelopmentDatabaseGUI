using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestDevelopmentDatabaseGUI.Services;


public sealed class GlobalObjects
{
    private static readonly object _syncRoot = new object();
    private static volatile GlobalObjects _instance;

    public static GlobalObjects Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new GlobalObjects();
                    }
                }
            }
            return _instance;
        }
    }

    private GlobalObjects()
    {

    }

    // Example of a global variable
    private SqlConnHandler _sqlConnHandler;

    public SqlConnHandler sqlConnHandler
    {
        get { return _sqlConnHandler; }
        set { _sqlConnHandler = value; }
    }
}

