using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpLogger;
using Logger = SharpLogger.Logger;

public static class Configuration
{
    public static bool AutostartServer = false;
    public static string LoggerFilter = "";

    public static void Initialize()
    {
        Logger.Log(LogLevel.L2_Info, "Configuration initialized.");
    }
}
