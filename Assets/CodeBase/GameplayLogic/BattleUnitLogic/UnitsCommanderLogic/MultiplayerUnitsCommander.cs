using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using CodeBase.NetworkLogic.EventsManagerLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic
{
    public class MultiplayerUnitsCommander : UnitsCommander,IOnEventCallback
    {
        private INetworkManager _networkManager;
        private INetworkEventsManager _networkEventsManager;

        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkEventsManager = ServiceLocator.Get<INetworkEventsManager>();

            
            _networkManager.AddCallbackTarget(this);
        }

        public override void SelectUnit(Vector2Int index)
        {
            base.SelectUnit(index);
            
            _networkEventsManager.RaiseSelectUnitEvent(index);
        }

        public override void MoveUnit(Vector2Int newIndex)
        {
            base.MoveUnit(newIndex);
            
            _networkEventsManager.RaiseMoveUnitEvent(newIndex);
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkEventsManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.SelectUnit) base.SelectUnit(_networkEventsManager.GetSelectUnitEventValue(photonEvent));
            else if (type == NetworkEventType.MoveUnit) base.MoveUnit(_networkEventsManager.GetMoveUnitEventValue(photonEvent));
        }
    }
}