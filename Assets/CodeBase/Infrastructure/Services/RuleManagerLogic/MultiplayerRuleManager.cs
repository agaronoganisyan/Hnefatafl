using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public class MultiplayerRuleManager : RuleManager
    {
        private INetworkManager _networkManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
        }

        protected override bool IsCanStartGame()
        {
            return _networkManager.IsCurrentRoomFull();
        }
    }
}