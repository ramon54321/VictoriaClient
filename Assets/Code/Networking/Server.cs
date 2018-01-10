using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ether.Network;
using Ether.Network.Packets;
using SharpLogger;
using VictoriaShared.Networking;
using Logger = SharpLogger.Logger;

public class Server : NetClient
{
    public Server(string host, int port, int bufferSize) : base(host, port, bufferSize)
    {
        
    }

    protected override void HandleMessage(NetPacketBase packet)
    {
        try
        {
            // -- Create datablock from string
            DataBlock dataBlock = new DataBlock(Encoding.ASCII.GetBytes(packet.Read<string>()));

            // -- Pass datablock to datablock controller
            NetworkManager.GetInstance().ProcessDataBlock(dataBlock, null);
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
        
    }

    public void SendDataBlock(DataBlock dataBlock)
    {
        using (NetPacket packet = new NetPacket())
        {
            // -- Create string from datablock bytes
            string datablockString = Encoding.ASCII.GetString(dataBlock.GetBytes());
            Logger.Log(LogLevel.L2_Info, "Sending datablock to server: " + datablockString, "NetworkMessages.FromClient");
            packet.Write(datablockString);
            this.Send(packet);
        }
    }

    protected override void OnConnected()
    {
        Logger.Log(LogLevel.L2_Info, "Connected to " + this.Socket.RemoteEndPoint.ToString(), "Network.Connect");
    }

    protected override void OnDisconnected()
    {
        Logger.Log(LogLevel.L2_Info, "Disconnected.", "Network.Disconnect");
    }
}
