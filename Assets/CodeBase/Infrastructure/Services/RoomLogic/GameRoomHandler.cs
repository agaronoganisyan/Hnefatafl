using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public abstract class GameRoomHandler : IGameRoomHandler
    {
        protected IRuleManager _ruleManager;

        public IGameRoomHandlerMediator GameRoomHandlerMediator => _gameRoomHandlerMediator;
        private IGameRoomHandlerMediator _gameRoomHandlerMediator;
        
        public virtual void Initialize()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();

            _gameRoomHandlerMediator = new GameRoomHandlerMediator();
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