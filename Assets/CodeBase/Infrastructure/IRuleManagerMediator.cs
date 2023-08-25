using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure
{
    public interface IRuleManagerMediator : IService
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