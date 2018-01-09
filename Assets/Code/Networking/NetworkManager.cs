using Ether.Network;
using VictoriaShared.Networking;

public class NetworkManager
{
    private static NetworkManager instance = null;
    public static NetworkManager GetInstance()
    {
        if (instance == null)
            instance = new NetworkManager();

        return instance;
    }
    private NetworkManager()
    {
        dataBlockController = new ClientDataBlockController();
    }

    private DataBlockController dataBlockController;

    private const string SERVER_IP = "127.0.0.1";
    private const int SERVER_PORT = 8888;
    private const int BUFFER_SIZE = 4096;

    private Server server = null;

    public void SetUpServerConnection()
    {
        if (server != null)
            return;

        server = new Server(SERVER_IP, SERVER_PORT, BUFFER_SIZE);
    }

    public void ConnectToServer()
    {
        if (server == null)
            return;

        if (server.IsConnected)
            return;

        server.Connect();
    }

    public void DisconnectFromServer()
    {
        if (server == null)
            return;

        if (!server.IsConnected)
            return;

        server.Disconnect();
        server = null;
    }

    public void SendDataBlock(DataBlock dataBlock)
    {
        if (server == null)
            return;

        if (!server.IsConnected)
            return;

        server.SendDataBlock(dataBlock);
    }

    public void ProcessDataBlock(DataBlock dataBlock, NetConnection netConnection)
    {
        dataBlockController.ProcessDataBlock(dataBlock, netConnection);
    }
}
