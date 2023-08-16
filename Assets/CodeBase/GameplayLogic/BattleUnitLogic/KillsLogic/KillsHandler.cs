using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.TileLogic;
using System;
using CodeBase.GameplayLogic.BoardLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class KillsHandler 
    {
        GameManager _gameManager;
        IBoardTilesContainer _board;
        UnitsManager _unitsManager;
        IUnitsStateContainer _unitsStateContainer;

        WayToKill _wayToKillKing;
        WayToKill _wayToKillWarrior;

        public KillsHandler(GameManager gameManager,IBoardTilesContainer board, UnitsManager unitsManager)
        {
            _gameManager = gameManager;
            _board = board;
            _unitsManager = unitsManager;

            _wayToKillKing = new WayToKillKing(_board, _unitsManager);
            _wayToKillWarrior = new WayToKillWarrior(_board, _unitsManager);
        }

        public void TryToKill(BattleUnit unit, Tile finalTile)
        {
            TeamType currentUnitTeamType = unit.TeamType;
            Vector2Int finalTileIndex = finalTile.Index;

            //Down
            FindTargetToKill(currentUnitTeamType, finalTileIndex, new Vector2Int(0, -1));

            //Up
            FindTargetToKill(currentUnitTeamType, finalTileIndex, new Vector2Int(0, 1));

            //Left
            FindTargetToKill(currentUnitTeamType, finalTileIndex, new Vector2Int(-1, 0));

            //Right
            FindTargetToKill(currentUnitTeamType, finalTileIndex, new Vector2Int(1, 0));
        }

        void FindTargetToKill(TeamType currentTeam, Vector2Int currentIndex, Vector2Int direction)
        {
            Vector2Int neighboringIndex = currentIndex + direction;
            if (_board.IsIndexOnBoard(neighboringIndex))
            {
                BattleUnit caughtUnit = _unitsStateContainer.GetUnitByIndex(neighboringIndex);

                if (caughtUnit != null && caughtUnit.TeamType != currentTeam)
                {
                    UnitType caughtUnitType = caughtUnit.UnitType;

                    if (caughtUnitType == UnitType.King)
                    {
                        _wayToKillKing.TryToKill(neighboringIndex, currentTeam, caughtUnitType, () => KillUnit(caughtUnit));
                    }
                    else
                    {
                        _wayToKillWarrior.TryToKill(neighboringIndex, currentTeam, caughtUnitType, () => KillUnit(caughtUnit), direction);
                    }
                }
            }
        }

        void KillUnit(BattleUnit unit)
        {
            _unitsStateContainer.RemoveUnitFromTile(unit);
            unit.Kill();

            if (unit.UnitType == UnitType.King) _gameManager.BlackTeamWin();
        }
    }
}
