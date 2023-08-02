using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsPlacementHandler 
    {
        GameManager _gameManager;
        Board _board;
        UnitsManager _unitsManager;

        KillsHandler _killsHandler;

        public UnitsPlacementHandler(GameManager gameManager, Board board, UnitsManager unitsManager)
        {
            _gameManager = gameManager;
            _board = board;
            _unitsManager = unitsManager;

            _killsHandler = new KillsHandler(_gameManager, _board, _unitsManager);
        }

        public void ProcessUnitPlacement(BattleUnit placedUnit, Tile finalTile)
        {
            _killsHandler.TryToKill(placedUnit, finalTile);

            if (_gameManager.IsGameFinished) return;

            if (placedUnit.UnitType == UnitType.King && finalTile.Type == TileType.Shelter) _gameManager.WhiteTeamWin();

            if (_gameManager.IsGameFinished) return;

            if (placedUnit.TeamType == TeamType.White)
            {
                if (!_unitsManager.IsThisTeamHaveaAnyAvailableMoves(TeamType.Black)) _gameManager.WhiteTeamWin();
            }
            else
            {
                if (!_unitsManager.IsThisTeamHaveaAnyAvailableMoves(TeamType.White)) _gameManager.BlackTeamWin();
            }
        }
    }
}