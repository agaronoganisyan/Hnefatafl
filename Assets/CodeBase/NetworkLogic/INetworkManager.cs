using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using ExitGames.Client.Photon;
using UnityEngine;

namespace CodeBase.NetworkLogic
{
    public enum NetworkEventType
    {
        None,
        SelectUnit,
        MoveUnit,
        TryToStartGame
    }

    public interface INetworkManager : IService
    {
        INetworkManagerMediator NetworkManagerMediator { get; }
        void ConnectToServer();
        void CreateRoom();
        void JoinRandomRoom();
        void SelectPlayerTeam(TeamType teamType);
        TeamType IsInCurrentRoomTeamWasSelected();
        TeamType GetPlayerTeam();
        bool IsConnected();
        bool IsCurrentRoomFull();
        void AddCallbackTarget(object target);
        void RaiseSelectUnitEvent(Vector2Int index);
        void RaiseMoveUnitEvent(Vector2Int index);
        void RaiseTryToStartGameEvent();
        NetworkEventType GetNetworkEventType(EventData photonEvent);
        Vector2Int GetSelectUnitEventValue(EventData photonEvent);
        Vector2Int GetMoveUnitEventValue(EventData photonEvent);
    }
}