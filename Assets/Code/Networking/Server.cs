using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ether.Network;
using Ether.Network.Packets;
using VictoriaShared.Networking;

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
            packet.Write(Encoding.ASCII.GetString(dataBlock.GetBytes()));
            this.Send(packet);
        }
    }

    protected override void OnConnected()
    {
        Debug.Log("Connected to " + this.Socket.RemoteEndPoint.ToString());
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected");
    }
}
