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
        }

        public override void TryToStartGame()
        {
            base.TryToStartGame();

            _networkManager.RaiseTryToStartGameEvent();
        }

        public override void Quit()
        {
            base.Quit();
            
            _networkManager.LeaveRoom();
        }

        protected override bool IsGameCanBeStarted()
        {
            Debug.LogError($"_networkManager.IsCurrentRoomFull() {_networkManager.IsCurrentRoomFull()} " +
                      $"_networkManager.IsAllPlayersInRoomSelectTeam() {_networkManager.IsAllPlayersInRoomSelectTeam()}");
            return _networkManager.IsCurrentRoomFull() && _networkManager.IsAllPlayersInRoomSelectTeam();
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.TryToStartGame) base.TryToStartGame();
        }
    }
}