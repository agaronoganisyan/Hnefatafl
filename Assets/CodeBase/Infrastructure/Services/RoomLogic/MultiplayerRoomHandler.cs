using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using CodeBase.NetworkLogic.EventsManagerLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using CodeBase.NetworkLogic.PlayerLogic;
using CodeBase.NetworkLogic.RoomManagerLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public class MultiplayerRoomHandler : GameRoomHandler , IOnEventCallback
    {
        private INetworkManager _networkManager;
        private INetworkEventsManager _networkEventsManager;
        private INetworkRoomManager _networkRoomManager;
        private INetworkPlayerManager _networkPlayerManager;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkEventsManager = ServiceLocator.Get<INetworkEventsManager>();
            _networkRoomManager = ServiceLocator.Get<INetworkRoomManager>();
            _networkPlayerManager= ServiceLocator.Get<INetworkPlayerManager>();
            
            _networkManager.AddCallbackTarget(this);

            _networkRoomManager.Mediator.OnRoomLeaved += QuitFromMultiplayerRoom;
            _networkRoomManager.Mediator.OnOpponentLeavedRoom += OpponentLeaveRoom;
        }

        public override void TryToStartGame()
        {
            TryToStartMultiplayerGame();

            _networkEventsManager.RaiseTryToStartGameEvent();
        }

        void TryToStartMultiplayerGame()
        {
            if (IsGameCanBeStarted())
            {
                _ruleManager.StartGame();
                
                _networkRoomManager.MarkRoomAsGameStarted(_networkRoomManager.GetCurrentRoom());
            }
        }

        public override void Quit()
        {
            LocalPlayerLeaveRoom();
            
            _networkRoomManager.LeaveRoom();
        }

        protected override bool IsGameCanBeStarted()
        {
            return _networkRoomManager.IsRoomFull(_networkRoomManager.GetCurrentRoom()) && _networkRoomManager.IsAllPlayersInRoomSelectTeam(_networkRoomManager.GetCurrentRoom());
        }

        private void QuitFromMultiplayerRoom()
        {
            base.Quit();
        }

        void LocalPlayerLeaveRoom()
        {
            if (_ruleManager.IsGameFinished) return;
            _ruleManager.SetWinningTeam(GetOppositeTeamType(_networkPlayerManager.GetPlayerTeam(_networkPlayerManager.GetLocalPlayer())));
        }
        
        void OpponentLeaveRoom()
        {
            if (_ruleManager.IsGameFinished) return;
            _ruleManager.SetWinningTeam(_networkPlayerManager.GetPlayerTeam(_networkPlayerManager.GetLocalPlayer()));
        }
        
        private TeamType GetOppositeTeamType(TeamType localPlayerTeamType)
        {
            return localPlayerTeamType == TeamType.White ? TeamType.Black : TeamType.White;
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkEventsManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.TryToStartGame) TryToStartMultiplayerGame();
        }
    }
}