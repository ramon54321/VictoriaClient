using VictoriaShared.Game;
using VictoriaShared.Game.NetworkObjects;
using VictoriaShared.Networking;
using VictoriaShared.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        Debug.Log("Timeline Time: " + timelineTime);
    }

    protected override void AddNetworkObject(NetworkObject networkObject)
    {
        Debug.Log("before add");
        networkObjects.Add(networkObject.ID, networkObject);
        Debug.Log("Adding ob");
        GameManager.GetInstance().ObjectAdded(networkObject);
    }
    protected override void RemoveNetworkObject(uint id)
    {
        GameManager.GetInstance().ObjectRemoved(id);
        networkObjects.Remove(id);
    }
}
