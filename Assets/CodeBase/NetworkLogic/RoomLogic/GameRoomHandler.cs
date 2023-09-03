using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.NetworkLogic.RoomLogic
{
    public abstract class GameRoomHandler : IGameRoomHandler
    {
        private IRuleManager _ruleManager;
        
        public virtual void Initialize()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
        }

        public virtual void TryToStartGame()
        {
            if (IsGameCanBeStarted())_ruleManager.StartGame();
        }

        public virtual void Quit()
        {
            
        }

        protected abstract bool IsGameCanBeStarted();
    }
}