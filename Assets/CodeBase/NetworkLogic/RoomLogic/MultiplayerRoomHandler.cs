using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

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

        protected override bool IsGameCanBeStarted()
        {
            return _networkManager.IsCurrentRoomFull();
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.TryToStartGame) base.TryToStartGame();
        }
    }
}