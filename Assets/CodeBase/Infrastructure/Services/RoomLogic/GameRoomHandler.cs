using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public abstract class GameRoomHandler : IGameRoomHandler, IGameplayModeChangingObserver
    {
        protected IRuleManager _ruleManager;

        public IGameRoomHandlerMediator Mediator => _gameRoomHandlerMediator;
        private IGameRoomHandlerMediator _gameRoomHandlerMediator;
        
        public virtual void Initialize()
        {
            _gameRoomHandlerMediator = new GameRoomHandlerMediator();
            
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
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