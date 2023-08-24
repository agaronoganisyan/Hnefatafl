using System;

namespace CodeBase.Infrastructure
{
    public class RuleManager : IRuleManager
    {
        private IRuleManagerMediator _managerMediator;
        
        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;

        public RuleManager(IRuleManagerMediator managerMediator)
        {
            _managerMediator = managerMediator;
        }
        
        public void StartGame()
        {
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
    }
}