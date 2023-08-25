using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComander : IService
    {
        void SelectUnit(Vector2Int index);
        void MoveUnit(Vector2Int newIndex);
    }
}