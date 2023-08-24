using System;

namespace CodeBase.Infrastructure
{
    public interface IRuleManager
    {
        bool IsGameFinished { get; }

        void StartGame();

        void RestartGame();

        void BlackTeamWin();

        void WhiteTeamWin();
    }
}