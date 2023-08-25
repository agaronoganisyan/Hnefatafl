using System.Collections.Generic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class KillsHandler : IKillsHandler
    {
        IBoardTilesContainer _boardContainer;
        IUnitsStateContainer _unitsStateContainer;
        
        private readonly Dictionary<UnitType, WayToKill> _waysToKill = new Dictionary<UnitType, WayToKill>();

        public void Initialize()
        {
            _boardContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
        }

        public void AddWayToKill(UnitType unitType, WayToKill wayToKill)
        {
            _waysToKill.Add(unitType, wayToKill);
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
            if (_boardContainer.IsIndexOnBoard(neighboringIndex))
            {
                BattleUnit caughtUnit = _unitsStateContainer.GetUnitByIndex(neighboringIndex);

                if (caughtUnit != null && caughtUnit.TeamType != currentTeam)
                {
                    UnitType caughtUnitType = caughtUnit.UnitType;

                    if (caughtUnitType == UnitType.King)
                    {
                        GetWayToKill(caughtUnitType).TryToKill(neighboringIndex, currentTeam, caughtUnitType, () => KillUnit(caughtUnit));
                    }
                    else
                    {
                        GetWayToKill(caughtUnitType).TryToKill(neighboringIndex, currentTeam, caughtUnitType, () => KillUnit(caughtUnit), direction);
                    }
                }
            }
        }

        WayToKill GetWayToKill(UnitType unitType)
        {
            return _waysToKill.TryGetValue(unitType, out var wayToKill) ? wayToKill : null;
        }

        void KillUnit(BattleUnit unit)
        {
            _unitsStateContainer.RemoveUnitFromTile(unit);
            unit.Kill();
        }
    }
}
