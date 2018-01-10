using UnityEngine;
using System.Collections;
using Logger = SharpLogger.Logger;

/**
 * This is the entry point to the game. This script will run before all other scripts.
 */
public class GameEntry : MonoBehaviour
{
    void Awake()
    {
        // -- Initialize Logger
        Logger.SetPrinter(new Printer());

        // -- Initialize the Configuration class
        Configuration.Initialize();

        // -- Set Logger filter from Configuration
        Logger.SetFilter(Configuration.LoggerFilter);

        // -- Spawn an instance of Network Manager (This will run the constructor)
        NetworkManager.GetInstance();
    }
}
