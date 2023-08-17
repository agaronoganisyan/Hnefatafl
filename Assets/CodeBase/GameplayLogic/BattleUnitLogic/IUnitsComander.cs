using System;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComander
    {
        event Action OnUnitSelected;
        event Action OnUnitUnselected;

        void SelectUnit(Vector2Int index);
        void MoveUnit(Vector2Int newIndex);
    }
}