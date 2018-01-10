using VictoriaShared.Game;
using VictoriaShared.Game.NetworkObjects;

public class ClientGameState : GameState
{
    private static ClientGameState _instance;
    public static ClientGameState GetInstance()
    {
        if (_instance == null)
            _instance = new ClientGameState();

        return _instance;
    }
    private ClientGameState()
    {
        
    }

    protected override void NetworkObjectAdded(NetworkObject networkObject)
    {
        throw new System.NotImplementedException();
    }

    protected override void NetworkObjectRemoved(NetworkObject networkObject)
    {
        throw new System.NotImplementedException();
    }
}