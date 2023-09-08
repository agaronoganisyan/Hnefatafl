using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.PlayerLogic;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MultiplayerInputHandler : InputHandler
    {
        private INetworkPlayerManager _networkPlayerManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkPlayerManager = ServiceLocator.Get<INetworkPlayerManager>();
        }
        
        protected override bool IsCanProcessClick()
        {
            return _turnManager.TeamOfTurn == _networkPlayerManager.GetPlayerTeam(_networkPlayerManager.GetLocalPlayer());
        }
    }
}