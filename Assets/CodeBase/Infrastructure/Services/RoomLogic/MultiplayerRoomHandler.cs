using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public class MultiplayerRoomHandler : GameRoomHandler , IOnEventCallback
    {
        private INetworkManager _networkManager;
        public override void Initialize()
        {
            base.Initialize();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.AddCallbackTarget(this);

            _networkManager.Mediator.OnRoomLeaved += QuitFromMultiplayerRoom;
            _networkManager.Mediator.OnOpponentLeavedRoom += OpponentLeaveRoom;
        }

        public override void TryToStartGame()
        {
            if (IsGameCanBeStarted())
            {
                _ruleManager.StartGame();
                
                _networkManager.MarkRoomAsGameStarted(_networkManager.GetCurrentRoom());
            }

            _networkManager.RaiseTryToStartGameEvent();
        }

        public override void Quit()
        {
            LocalPlayerLeaveRoom();
            
            _networkManager.LeaveRoom();
        }

        protected override bool IsGameCanBeStarted()
        {
            return _networkManager.IsRoomFull(_networkManager.GetCurrentRoom()) && _networkManager.IsAllPlayersInRoomSelectTeam(_networkManager.GetCurrentRoom());
        }

        private void QuitFromMultiplayerRoom()
        {
            base.Quit();
        }

        void LocalPlayerLeaveRoom()
        {
            if (_ruleManager.IsGameFinished) return;
            _ruleManager.SetWinningTeam(GetOppositeTeamType(_networkManager.GetPlayerTeam(_networkManager.GetLocalPlayer())));
        }
        
        void OpponentLeaveRoom()
        {
            if (_ruleManager.IsGameFinished) return;
            _ruleManager.SetWinningTeam(_networkManager.GetPlayerTeam(_networkManager.GetLocalPlayer()));
        }
        
        private TeamType GetOppositeTeamType(TeamType localPlayerTeamType)
        {
            return localPlayerTeamType == TeamType.White ? TeamType.Black : TeamType.White;
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.TryToStartGame) base.TryToStartGame();
        }
    }
}