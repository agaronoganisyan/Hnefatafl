using System;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComander
    {
        void SelectUnit(Vector2Int index);
        void MoveUnit(Vector2Int newIndex);
    }
}