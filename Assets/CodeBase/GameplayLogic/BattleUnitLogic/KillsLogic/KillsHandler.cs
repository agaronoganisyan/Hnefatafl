using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BoardLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class KillsHandler : IKillsHandler
    {
        IBoardTilesContainer _board;
        IUnitsStateContainer _unitsStateContainer;

        WayToKill _wayToKillKing;
        WayToKill _wayToKillWarrior;

        public KillsHandler(IBoardTilesContainer board,IUnitsStateContainer unitsStateContainer, WayToKill wayToKillKing, WayToKill wayToKillWarrior)
        {
            _board = board;

            _wayToKillKing = wayToKillKing;
            _wayToKillWarrior = wayToKillWarrior;
            _unitsStateContainer = unitsStateContainer;
        }

        public void FindTargetsToKill(BattleUnit unit)
        {
            TeamType currentUnitTeamType = unit.TeamType;
            Vector2Int finalTileIndex = unit.Index;
            
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
        }
    }
}
