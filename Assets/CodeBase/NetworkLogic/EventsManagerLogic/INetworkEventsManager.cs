using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using ExitGames.Client.Photon;
using UnityEngine;

namespace CodeBase.NetworkLogic.EventsManagerLogic
{
    public interface INetworkEventsManager : IService
    {
        void RaiseSelectUnitEvent(Vector2Int index);
        void RaiseMoveUnitEvent(Vector2Int index);
        void RaiseTryToStartGameEvent();
        void RaiseSelectTeamEvent(TeamType teamType);
        void RaiseFinishGameEvent(TeamType teamType);
        
        NetworkEventType GetNetworkEventType(EventData photonEvent);
        Vector2Int GetSelectUnitEventValue(EventData photonEvent);
        Vector2Int GetMoveUnitEventValue(EventData photonEvent);
        TeamType GetSelectTeamEventValue(EventData photonEvent);
        TeamType GetFinishGameEventValue(EventData photonEvent);
    }
}