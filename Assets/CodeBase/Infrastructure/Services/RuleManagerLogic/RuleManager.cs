using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public abstract class RuleManager : IRuleManager
    {
        public IRuleManagerMediator RuleManagerMediator => _managerMediator;
        private IRuleManagerMediator _managerMediator;
        
        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;
        
        public virtual void Initialize()
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

        public virtual void SetWinningTeam(TeamType teamType)
        {
            if (teamType == TeamType.White) WhiteTeamWin();
            else if (teamType == TeamType.Black) BlackTeamWin();
        }

        private void BlackTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;
            
            _managerMediator.NotifyAboutBlackTeamWon();
        }

        private void WhiteTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;

            _managerMediator.NotifyAboutWhiteTeamWon();
        }
    }
}