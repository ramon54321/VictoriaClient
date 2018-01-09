using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictoriaShared.Networking;
using Ether.Network;
using System;

public class ClientDataBlockController : DataBlockController
{
    protected override void Action(DataBlock dataBlock, NetConnection netConnection)
    {
        // Type 2
        ClientGameStateManager.GetInstance().AddDatablock(dataBlock);
    }

    protected override void ActionRequest(DataBlock dataBlock, NetConnection netConnection)
    {
        // Type 1
        throw new NotImplementedException();
    }

    protected override void Admin(DataBlock dataBlock, NetConnection netConnection)
    {
        try
        {
            // -- Admin messages - Type 0
            string[] data = dataBlock.body.Split('#');

            // -- Console Log
            if (data[0] == "cl")
            {
                Debug.Log("Server LOGS: " + data[1]);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error Parsing: " + dataBlock.body);
        }
    }
}
