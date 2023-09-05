using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public class MultiplayerRuleManager : RuleManager , IOnEventCallback
    {
        private INetworkManager _networkManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.AddCallbackTarget(this);
        }


        public override void SetWinningTeam(TeamType teamType)
        {
            _networkManager.RaiseFinishGameEvent(teamType);

            base.SetWinningTeam(teamType);
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.FinishGame) base.SetWinningTeam(_networkManager.GetFinishGameEventValue(photonEvent));
        }
    }
}