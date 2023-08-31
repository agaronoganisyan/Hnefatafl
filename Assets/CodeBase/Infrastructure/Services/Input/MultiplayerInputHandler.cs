using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MultiplayerInputHandler : InputHandler
    {
        private INetworkManager _networkManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
        }
        
        protected override bool IsCanProcessClick()
        {
            return _turnManager.TeamOfTurn == _networkManager.GetPlayerTeam();
        }
    }
}