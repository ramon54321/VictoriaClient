using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ether.Network;
using SharpLogger;
using VictoriaShared.Networking;
using Logger = SharpLogger.Logger;

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
                Logger.Log(LogLevel.L2_Info, "Server Message: " + data[1], "NetworkMessages.FromServer");
            }
        }
        catch (Exception e)
        {
            Logger.Log(LogLevel.L4_RecoverableError, "Error parsing message from server: " + dataBlock.body, "NetworkMessages.FromServer");
        }
    }
}
