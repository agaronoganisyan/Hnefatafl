using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure
{
    public interface IRuleManager : IService
    {
        IRuleManagerMediator RuleManagerMediator { get; }
        bool IsGameFinished { get; }
        void StartGame();
        void RestartGame();
        void BlackTeamWin();
        void WhiteTeamWin();
    }
}