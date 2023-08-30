namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public abstract class RuleManager : IRuleManager
    {
        public IRuleManagerMediator RuleManagerMediator => _managerMediator;
        private IRuleManagerMediator _managerMediator;
        
        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;
        
        public virtual void Initialize()
        {
            _managerMediator = new RuleManagerMediator();
        }
        
        public void StartGame()
        {
            if (!IsCanStartGame()) return;
            
            _isGameFinished = false;

            _managerMediator.NotifyAboutStartedGame();
        }

        public void RestartGame()
        {
            _managerMediator.NotifyAboutRestartedGame();
            StartGame();
        }

        public void BlackTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;
            
            _managerMediator.NotifyAboutBlackTeamWon();
        }

        public void WhiteTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;

            _managerMediator.NotifyAboutWhiteTeamWon();
        }

        protected abstract bool IsCanStartGame();
    }
}