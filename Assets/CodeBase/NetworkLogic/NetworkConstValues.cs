using Photon.Realtime;

namespace CodeBase.NetworkLogic
{
    public static class NetworkConstValues
    {
        public const string KEY_TEAM = "Team";
        
        public static TypedLobby DefaultLobby = new TypedLobby("defaultLobby", LobbyType.Default);

    }
}