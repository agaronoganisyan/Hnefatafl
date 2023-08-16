using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitPath
    {
        public Vector2Int CurrentIndex { get;}
        public IReadOnlyList<Vector2Int> AvailableMoves{ get;}
        public void SetCurrentIndex(Vector2Int index);
        public void AddAvailableMove(Vector2Int index);

        public bool IsPathHasIndex(Vector2Int index);
    }
}