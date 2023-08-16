using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
     class UnitPath : IUnitPath
    {
        private Vector2Int _currentIndex;
        public Vector2Int CurrentIndex => _currentIndex;

        List<Vector2Int> _availableMoves = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> AvailableMoves => _availableMoves;

        public void SetCurrentIndex(Vector2Int index)
        {
            _currentIndex = index;
        }

        public void AddAvailableMove(Vector2Int index)
        {
            _availableMoves.Add(index);
        }

        public bool IsPathHasIndex(Vector2Int index)
        {
            return _availableMoves.Contains(index);
        }
    }
}