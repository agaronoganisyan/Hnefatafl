using System;

namespace CodeBase.Infrastructure
{
    public interface IRuleManager
    {
        event Action OnGameStarted;
        event Action OnGameRestarted;
        event Action OnGameFinished;
        event Action OnWhiteTeamWon;
        event Action OnBlackTeamWon;

        bool IsGameFinished { get; }

        void StartGame();

        void RestartGame();

        void BlackTeamWin();

        void WhiteTeamWin();
    }
}