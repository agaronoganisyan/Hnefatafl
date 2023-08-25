using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsStateContainer : IService
    {
        void GenerateContainer();
        void RemoveUnitFromTile(BattleUnit unit);
        bool IsThereUnit(Vector2Int index);
        BattleUnit GetUnitByIndex(Vector2Int neighboringIndex);
        void AddUnitToTile(BattleUnit intsUnit, Vector2Int index);
        void ChangeUnitIndex(BattleUnit unit, Vector2Int newIndex);
        void Clear();
    }
}