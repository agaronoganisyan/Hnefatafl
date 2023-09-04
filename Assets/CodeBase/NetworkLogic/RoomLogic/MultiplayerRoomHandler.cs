using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.NetworkLogic.RoomLogic
{
    public class MultiplayerRoomHandler : GameRoomHandler , IOnEventCallback
    {
        private INetworkManager _networkManager;
        public override void Initialize()
        {
            base.Initialize();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.AddCallbackTarget(this);

            _networkManager.NetworkManagerMediator.OnLeftRoom += base.Quit;
        }

        public override void TryToStartGame()
        {
            base.TryToStartGame();

            if (IsGameCanBeStarted())
            {
                _networkManager.MarkRoomAsGameStarted(_networkManager.GetCurrentRoom());
            }

            _networkManager.RaiseTryToStartGameEvent();
        }

        public override void Quit()
        {
            _networkManager.LeaveRoom();
        }

        protected override bool IsGameCanBeStarted()
        {
            return _networkManager.IsRoomFull(_networkManager.GetCurrentRoom()) && _networkManager.IsAllPlayersInRoomSelectTeam(_networkManager.GetCurrentRoom());
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.TryToStartGame) base.TryToStartGame();
        }
    }
}