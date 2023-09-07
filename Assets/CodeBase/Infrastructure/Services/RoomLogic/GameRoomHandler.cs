using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public abstract class GameRoomHandler : IGameRoomHandler, IGameplayModeChangingObserver
    {
        protected IRuleManager _ruleManager;

        public IGameRoomHandlerMediator Mediator => _gameRoomHandlerMediator;
        private IGameRoomHandlerMediator _gameRoomHandlerMediator;
        
        public virtual void Initialize()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
            
            _gameRoomHandlerMediator = new GameRoomHandlerMediator();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
        }
        
        public void UpdateChangedProperties()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
        }
        
        public virtual void TryToStartGame()
        {
            if (IsGameCanBeStarted()) _ruleManager.StartGame();
        }

        public virtual void Quit()
        {
            _gameRoomHandlerMediator.NotifyAboutQuittingRoom();
        }

        protected abstract bool IsGameCanBeStarted();
    }
}