using System;
using System.Collections.Generic;

namespace AndroidTestCross.Classes;

public static class ConfigHandler
{
    private static Dictionary<String, String> _configValues = new Dictionary<String, String>()
    {
        { "Path", "" },
        { "item2", "value2" }
    };
    
    public static void createConfig(String filePath)
    {
        _configValues["Path"] = filePath;
        writeConfig(_configValues);
    }

    public static void loadConfig()
    {
        // Implement load
    }

    public static void writeConfig(Dictionary<String, String> config)
    {
        // Implement write
    }

    public static String getConfigValue(String key)
    {
        return _configValues[key];
    }
}