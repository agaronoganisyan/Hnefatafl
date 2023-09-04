using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
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