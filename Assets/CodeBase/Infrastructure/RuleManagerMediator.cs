using System;

namespace CodeBase.Infrastructure
{
    public class RuleManagerMediator : IRuleManagerMediator
    {
        public event Action OnGameStarted;
        public event Action OnGameRestarted;
        public event Action OnGameFinished;
        public event Action OnWhiteTeamWon;
        public event Action OnBlackTeamWon;
        
        public void Initialize()
        {
            
        }
        
        public void NotifyAboutStartedGame()
        {
            OnGameStarted?.Invoke();
        }

        public void NotifyAboutRestartedGame()
        {
            OnGameRestarted?.Invoke();
        }

        public void NotifyAboutWhiteTeamWon()
        {
            OnWhiteTeamWon?.Invoke();
            OnGameFinished?.Invoke();
        }

        public void NotifyAboutBlackTeamWon()
        {
            OnBlackTeamWon?.Invoke();
            OnGameFinished?.Invoke();
        }
    }
}