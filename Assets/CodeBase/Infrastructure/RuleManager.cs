using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure
{
    public class RuleManager : IRuleManager
    {
        public IRuleManagerMediator RuleManagerMediator => _managerMediator;
        private IRuleManagerMediator _managerMediator;
        
        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;
        
        public void Initialize()
        {
            _managerMediator = new RuleManagerMediator();
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