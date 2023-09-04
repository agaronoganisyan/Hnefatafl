using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.NetworkLogic
{
    public enum NetworkEventType
    {
        None,
        SelectUnit,
        MoveUnit,
        TryToStartGame,
        SelectTeam
    }

    public interface INetworkManager : IService
    {
        INetworkManagerMediator NetworkManagerMediator { get; }
        void ConnectToServer();
        void CreateRoom(string roomName);
        void JoinRandomRoom();
        void JoinPrescribedRoom(string roomName);
        void LeaveRoom();
        void SelectTeamInRoom(TeamType teamType,Room room);
        void MarkRoomAsGameStarted(Room room);
        TeamType GetSelectedTeamInRoom(Room room);
        TeamType GetLocalPlayerTeam();
        bool IsConnected();
        bool IsInLobby();
        bool IsRoomFull(Room room);
        bool IsAllPlayersInRoomSelectTeam(Room room);
        void AddCallbackTarget(object target);
        void RaiseSelectUnitEvent(Vector2Int index);
        void RaiseMoveUnitEvent(Vector2Int index);
        void RaiseTryToStartGameEvent();
        void RaiseSelectTeamEvent(TeamType teamType);
        Room GetCurrentRoom();
        NetworkEventType GetNetworkEventType(EventData photonEvent);
        Vector2Int GetSelectUnitEventValue(EventData photonEvent);
        Vector2Int GetMoveUnitEventValue(EventData photonEvent);
        TeamType GetSelectTeamEventValue(EventData photonEvent);
    }
}