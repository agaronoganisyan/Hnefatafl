using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsPlacementHandler : IUnitsPlacementHandler
    {
        IGameManager _gameManager;
        IKillsHandler _killsHandler;
        private ITeamMoveValidator _teamMoveValidator;
        public UnitsPlacementHandler(IGameManager gameManager, IKillsHandler killsHandler,ITeamMoveValidator teamMoveValidator)
        {
            _gameManager = gameManager;
            _killsHandler = killsHandler;
            _teamMoveValidator = teamMoveValidator;
        }
        
        public void ProcessPlacement(BattleUnit placedUnit, TileType finalTileType)
        {
            _killsHandler.FindTargetsToKill(placedUnit);

            if (_gameManager.IsGameFinished) return;

            if (placedUnit.UnitType == UnitType.King && finalTileType == TileType.Shelter) _gameManager.WhiteTeamWin();

            if (_gameManager.IsGameFinished) return;

            if (placedUnit.TeamType == TeamType.White)
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.Black)) _gameManager.WhiteTeamWin();
            }
            else
            {
                if (!_teamMoveValidator.IsThisTeamHaveAnyAvailableMoves(TeamType.White)) _gameManager.BlackTeamWin();
            }
        }
    }
}