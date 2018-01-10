using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLogger;
using UnityEngine;
using VictoriaShared.Game;
using VictoriaShared.Game.NetworkObjects;
using VictoriaShared.Networking;
using VictoriaShared.Timeline;
using Logger = SharpLogger.Logger;

class ClientGameStateManager : GameStateManager
{
    public static ClientGameStateManager GetInstance()
    {
        if (instance == null)
            instance = new ClientGameStateManager();

        return (ClientGameStateManager) instance;
    }
    private ClientGameStateManager()
    {
        networkObjects = new Dictionary<uint, NetworkObject>();
        timelineTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        eventTimeline = new EventTimeline(timelineTime);

        Logger.Log(LogLevel.L2_Info, "Game state manager instantiated at time: " + timelineTime + ".", "Singletons.GameStateManager");
    }

    protected override void AddNetworkObject(NetworkObject networkObject)
    {
        Logger.Log(LogLevel.L2_Info, "GameStateManager: Adding network object to game state manager network object dictionary with id as key.", "Objects.Spawn");
        networkObjects.Add(networkObject.ID, networkObject);
        Logger.Log(LogLevel.L2_Info, "GameStateManager: Calling game manager ObjectAdded() method with network object as parameter.", "Objects.Spawn");
        GameManager.GetInstance().ObjectAdded(networkObject);
    }
    protected override void RemoveNetworkObject(uint id)
    {
        GameManager.GetInstance().ObjectRemoved(id);
        networkObjects.Remove(id);
    }
}
