using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsPlacementHandler : IUnitsPlacementHandler
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
        }
        
        public void ProcessPlacement(BattleUnit placedUnit, TileType finalTileType)
        {
            _killsHandler.FindTargetsToKill(placedUnit);

            if (!_teamsUnitsContainer.IsThisUnitTypeIsAlive(TeamType.White, UnitType.King))
            {
                _ruleManager.BlackTeamWin();
                return;
            }

            if (_ruleManager.IsGameFinished) return;

            if (placedUnit.UnitType == UnitType.King && finalTileType == TileType.Shelter) _ruleManager.WhiteTeamWin();

            if (_ruleManager.IsGameFinished) return;

            if (placedUnit.TeamType == TeamType.White)
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.Black)) _ruleManager.WhiteTeamWin();
            }
            else
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.White)) _ruleManager.BlackTeamWin();
            }
        }
    }
}