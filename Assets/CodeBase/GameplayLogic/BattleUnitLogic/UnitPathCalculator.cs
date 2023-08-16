using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public abstract class UnitPathCalculator
    {
        protected IBoardTilesContainer _boardTilesContainer;
        protected IUnitsStateContainer _unitsStateContainer;
        
        private Vector2Int _currentIndex;
        public Vector2Int CurrentIndex => _currentIndex;

        List<Vector2Int> _availableMoves = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> AvailableMoves => _availableMoves;
        
        public void CalculatePaths(Vector2Int currentIndex)
        {
            _availableMoves.Clear();
            
            //Down
            for (int i = currentIndex.y - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(currentIndex.x, i);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Up
            for (int i = currentIndex.y + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(currentIndex.x, i);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Left
            for (int i = currentIndex.x - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(i, currentIndex.y);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Right
            for (int i = currentIndex.x + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(i, currentIndex.y);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }
        }
        
        protected abstract bool IsThereProblemWithIndex(Vector2Int index);

        public bool IsThereAvailableMoves(Vector2Int currentIndex)
        {
            CalculatePaths(currentIndex);
            return _availableMoves.Count>0;
        }

        public bool IsThisIndexAvailableToMove(Vector2Int index)
        {
            return _availableMoves.Contains(index);
        }
    }
}