using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using CodeBase.NetworkLogic.EventsManagerLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public class MultiplayerRuleManager : RuleManager , IOnEventCallback
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


        public override void SetWinningTeam(TeamType teamType)
        {
            _networkEventsManager.RaiseFinishGameEvent(teamType);

            base.SetWinningTeam(teamType);
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkEventsManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.FinishGame) base.SetWinningTeam(_networkEventsManager.GetFinishGameEventValue(photonEvent));
        }
    }
}