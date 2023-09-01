using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using UnityEngine;

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
            Debug.Log($"IsCanProcessClick _turnManager.TeamOfTurn {_turnManager.TeamOfTurn} _networkManager.GetPlayerTeam() {_networkManager.GetPlayerTeam()}");
            
            return _turnManager.TeamOfTurn == _networkManager.GetPlayerTeam();
        }
    }
}