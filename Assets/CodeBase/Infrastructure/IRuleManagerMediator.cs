using System;

namespace CodeBase.Infrastructure
{
    public interface IRuleManagerMediator
    {
        event Action OnGameStarted;
        event Action OnGameRestarted;
        event Action OnGameFinished;
        event Action OnWhiteTeamWon;
        event Action OnBlackTeamWon;

        void NotifyAboutStartedGame();
        void NotifyAboutRestartedGame();
        void NotifyAboutWhiteTeamWon();
        void NotifyAboutBlackTeamWon();
    }
}