using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.TileLogic;
using System;
using CodeBase.GameplayLogic.BoardLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class KillsHandler 
    {
        GameManager _gameManager;
        Board _board;
        UnitsManager _unitsManager;

        public KillsHandler(GameManager gameManager,Board board, UnitsManager unitsManager)
        {
            _gameManager = gameManager;
            _board = board;
            _unitsManager = unitsManager;
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
                BattleUnit caughtUnit = _unitsManager.GetUnitByIndex(neighboringIndex);

                if (caughtUnit != null && caughtUnit.TeamType != currentTeam)
                {
                    UnitType caughtUnitType = caughtUnit.UnitType;

                    if (caughtUnitType == UnitType.King)
                    {
                        Vector2Int nextDownIndex = neighboringIndex + new Vector2Int(0, -1);
                        Vector2Int nextUpIndex = neighboringIndex + new Vector2Int(0, 1);
                        Vector2Int nextLeftIndex = neighboringIndex + new Vector2Int(-1, 0);
                        Vector2Int nextRightIndex = neighboringIndex + new Vector2Int(1, 0);

                        if (IsCaughtUnitSurroundedOnThisSide(nextDownIndex, currentTeam, caughtUnitType) &&
                            IsCaughtUnitSurroundedOnThisSide(nextUpIndex, currentTeam, caughtUnitType) &&
                            IsCaughtUnitSurroundedOnThisSide(nextLeftIndex, currentTeam, caughtUnitType) &&
                            IsCaughtUnitSurroundedOnThisSide(nextRightIndex, currentTeam, caughtUnitType)) KillUnit(caughtUnit);
                    }
                    else
                    {
                        Vector2Int nextIndex = neighboringIndex + direction;
                        if (IsCaughtUnitSurroundedOnThisSide(nextIndex, currentTeam, caughtUnitType)) KillUnit(caughtUnit);
                    }
                }
            }
        }

        void KillUnit(BattleUnit unit)
        {
            _unitsManager.RemoveUnitFromTile(unit);
            unit.Kill();

            if (unit.UnitType == UnitType.King) _gameManager.DefeatGame();
        }

        bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType)
        {
            if (_board.IsIndexOnBoard(index))
            {
                BattleUnit neighboringUnit = _unitsManager.GetUnitByIndex(index);

                if (neighboringUnit != null)
                {
                    if (neighboringUnit.TeamType == currentUnitTeam) return true;
                    else return false;
                }
                else
                {
                    TileType tileType = _board.GetTileTypeByIndex(index);

                    if (caughtUnitType == UnitType.King)
                    {
                        if (tileType == TileType.Thron) return true;
                        else return false;
                    }
                    else
                    {
                        if (tileType == TileType.Thron || tileType == TileType.Shelter) return true;
                        else return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
