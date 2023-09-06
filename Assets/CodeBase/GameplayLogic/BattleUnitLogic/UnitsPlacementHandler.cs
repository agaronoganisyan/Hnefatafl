using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsPlacementHandler : IUnitsPlacementHandler, IGameplayModeChangingObserver
    {
        private IRuleManager _ruleManager;
        private IKillsHandler _killsHandler;
        private ITeamMoveValidator _teamMoveValidator;
        private ITeamsUnitsContainer _teamsUnitsContainer;
        
        public void Initialize()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            _killsHandler  = ServiceLocator.Get<IKillsHandler>();
            _teamMoveValidator = ServiceLocator.Get<ITeamMoveValidator>();
            _teamsUnitsContainer  = ServiceLocator.Get<ITeamsUnitsContainer>();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
        }
        
        public void UpdateChangedProperties()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
        }
        
        public void ProcessPlacement(BattleUnit placedUnit, TileType finalTileType)
        {
            _killsHandler.FindTargetsToKill(placedUnit);

            if (!_teamsUnitsContainer.IsThisUnitTypeIsAlive(TeamType.White, UnitType.King))
            {
                _ruleManager.SetWinningTeam(TeamType.Black);
                return;
            }

            if (_ruleManager.IsGameFinished) return;

            if (placedUnit.UnitType == UnitType.King && finalTileType == TileType.Shelter) _ruleManager.SetWinningTeam(TeamType.White);

            if (_ruleManager.IsGameFinished) return;

            if (placedUnit.TeamType == TeamType.White)
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.Black)) _ruleManager.SetWinningTeam(TeamType.White);
            }
            else
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.White)) _ruleManager.SetWinningTeam(TeamType.Black);
            }
        }
    }
}