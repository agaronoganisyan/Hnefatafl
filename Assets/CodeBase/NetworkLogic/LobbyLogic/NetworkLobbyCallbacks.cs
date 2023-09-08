using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.LobbyLogic
{
    public class NetworkLobbyCallbacks : ILobbyCallbacks
    {
        public virtual void OnJoinedLobby()
        {
        }

        public virtual void OnLeftLobby()
        {
        }

        public virtual void OnRoomListUpdate(List<RoomInfo> roomList)
        {
        }

        public virtual void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }
    }
}