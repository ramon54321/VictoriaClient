using System;
using Ether.Network;
using VictoriaShared.Game;
using VictoriaShared.Networking;

public class NetworkManager
{
    private static NetworkManager _instance = null;
    public static NetworkManager GetInstance()
    {
        if (_instance == null)
            _instance = new NetworkManager();

        return _instance;
    }
    private NetworkManager()
    {
        _clientGameState = ClientGameState.GetInstance();

        if (Configuration.AutostartServer)
        {
            SetUpServerConnection();
            ConnectToServer();
        }
    }

    private const string SERVER_IP = "127.0.0.1";
    private const int SERVER_PORT = 8888;
    private const int BUFFER_SIZE = 4096;

    private NetworkSocket _networkSocket = null;
    private ClientGameState _clientGameState = null;

    public void SetUpServerConnection()
    {
        if (_networkSocket != null)
            return;

        _networkSocket = new NetworkSocket(SERVER_IP, SERVER_PORT, BUFFER_SIZE);
    }

    public void ConnectToServer()
    {
        if (_networkSocket == null)
            return;

        if (_networkSocket.IsConnected)
            return;

        _networkSocket.Connect();
    }

    public void DisconnectFromServer()
    {
        if (_networkSocket == null)
            return;

        if (!_networkSocket.IsConnected)
            return;

        _networkSocket.Disconnect();
        _networkSocket = null;
    }

    public void SendDataBlock(DataBlock dataBlock)
    {
        if (_networkSocket == null)
            return;

        if (!_networkSocket.IsConnected)
            return;

        _networkSocket.SendDataBlock(dataBlock);
    }

    /**
     * This method is called when a message is received from the server, and has been converted to a datablock object.
     *
     * Datablocks are split into 4 groups.
     *
     * Check which group the datablock belongs to.
     * Send Events and Actions to the GameState to deal with.
     * Send Admin datablocks to the AdminManager to deal with.
     */
    public void ProcessDataBlock(DataBlock dataBlock)
    {
        // -- Admin
        if (dataBlock.function < DataBlockFunction.ADMIN)
        {
            throw new NotImplementedException();
        }
        // -- Request
        else if (dataBlock.function < DataBlockFunction.REQUEST)
        {
            throw new NotImplementedException();
        }
        // -- Action
        else if (dataBlock.function < DataBlockFunction.ACTION)
        {
            _clientGameState.AddDatablockAction(dataBlock);
        }
        // -- Event
        else
        {
            _clientGameState.AddDatablockEvent(dataBlock);
        }
    }
}
