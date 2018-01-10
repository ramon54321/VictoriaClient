using System;
using System.Collections;
using System.Collections.Generic;
using SharpLogger;
using UnityEngine;
using VictoriaShared.Game.NetworkObjects;
using Logger = SharpLogger.Logger;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;

    private Dictionary<uint, GameObject> gameObjects;
    private volatile Queue<NetworkObject> spawnQueue;
    private ClientGameStateManager clientGameStateManager;
    private static GameManager instance = null;
    public static GameManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        SharpLogger.Logger.SetPrinter(new Printer());
        SharpLogger.Logger.SetFilter("");
        spawnQueue = new Queue<NetworkObject>();
        clientGameStateManager = ClientGameStateManager.GetInstance();
    }

    void Start ()
    {
        gameObjects = new Dictionary<uint, GameObject>();
	}

    public void ObjectAdded(NetworkObject networkObject)
    {
        // -- Add Object to spawn queue
        Logger.Log(LogLevel.L2_Info, "GameManager: Adding object to spawn queue.", "Objects.Spawn.GameManager");
        spawnQueue.Enqueue(networkObject);
    }
    public void ObjectRemoved(uint id)
    {
        if (!gameObjects.ContainsKey(id))
            return;

        Logger.Log(LogLevel.L2_Info, "GameManager: Removing object from game.", "Objects.Despawn.GameManager");

        // -- Despawn Gameobject
        GameObject go = gameObjects[id];
        gameObjects.Remove(id);
        Destroy(go);
    }
	
    private void ProcessQueues()
    {
        // -- Spawn Queue
        while(spawnQueue.Count > 0)
        {
            NetworkObject networkObject = spawnQueue.Dequeue();
            GameObject newObject = Instantiate<GameObject>(tank);
            gameObjects.Add(networkObject.ID, newObject);
        }
    }

	void Update ()
    {
        ProcessQueues();

        if (Input.GetKeyUp(KeyCode.Q))
        {
            foreach (uint id in gameObjects.Keys)
            {
                GameObject go = gameObjects[id];
                Unit no = (Unit)clientGameStateManager.GetNetworkObject(id);

                no.TimelinePositionX.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds(), 0);
                no.TimelinePositionX.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000, 10);
                no.TimelinePositionX.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000, -30);

                SortedList<long, float> points = no.TimelinePositionX.GetSortedList();

                foreach(long time in points.Keys)
                {
                    Debug.Log("Time: " + time + " : " + points[time]);
                }

                
            }
        }

        foreach(uint id in gameObjects.Keys)
        {
            GameObject go = gameObjects[id];
            Unit no = (Unit) clientGameStateManager.GetNetworkObject(id);
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            go.transform.position = new Vector3(no.TimelinePositionX.Get(time), 0, no.TimelinePositionY.Get(time));
        }
	}
}
