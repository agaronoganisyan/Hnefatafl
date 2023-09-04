using System;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
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