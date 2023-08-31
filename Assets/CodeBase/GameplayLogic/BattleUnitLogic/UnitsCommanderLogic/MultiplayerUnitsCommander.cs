using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic
{
    public class MultiplayerUnitsCommander : UnitsCommander,IOnEventCallback
    {
        private INetworkManager _networkManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
            
            _networkManager.AddCallbackTarget(this);
        }

        public override void SelectUnit(Vector2Int index)
        {
            base.SelectUnit(index);
            
            _networkManager.RaiseSelectUnitEvent(index);
        }

        public override void MoveUnit(Vector2Int newIndex)
        {
            base.MoveUnit(newIndex);
            
            _networkManager.RaiseMoveUnitEvent(newIndex);
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.SelectUnit) base.SelectUnit(_networkManager.GetSelectUnitEventValue(photonEvent));
            else if (type == NetworkEventType.MoveUnit) base.MoveUnit(_networkManager.GetMoveUnitEventValue(photonEvent));
        }
    }
}