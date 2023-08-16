using System.Collections.Generic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public abstract class UnitPathCalculator
    {
        protected IBoardTilesContainer _boardTilesContainer;
        protected IUnitsStateContainer _unitsStateContainer;
        
        public UnitPathCalculator(IBoardTilesContainer boardTilesContainer,IUnitsStateContainer unitsStateContainer)
        {
            _boardTilesContainer = boardTilesContainer;
            _unitsStateContainer = unitsStateContainer;
        }

        public IUnitPath CalculatePaths(Vector2Int currentIndex)
        {
            UnitPath path = new UnitPath();
            
            path.SetCurrentIndex(currentIndex);
            
            //Down
            for (int i = currentIndex.y - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(currentIndex.x, i);
                
                if (IsThereProblemWithIndex(index)) break;
                else path.AddAvailableMove(index);
            }

            //Up
            for (int i = currentIndex.y + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(currentIndex.x, i);

                if (IsThereProblemWithIndex(index)) break;
                else path.AddAvailableMove(index);
            }

            //Left
            for (int i = currentIndex.x - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(i, currentIndex.y);

                if (IsThereProblemWithIndex(index)) break;
                else path.AddAvailableMove(index);
            }

            //Right
            for (int i = currentIndex.x + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(i, currentIndex.y);

                if (IsThereProblemWithIndex(index)) break;
                else path.AddAvailableMove(index);
            }

            return path;
        }
        
        protected abstract bool IsThereProblemWithIndex(Vector2Int index);

        // public bool IsThereAvailableMoves(Vector2Int currentIndex)
        // {
        //     CalculatePaths(currentIndex);
        //     return _availableMoves.Count>0;
        // }
        //
        // public bool IsThisIndexAvailableToMove(Vector2Int index)
        // {
        //     return _availableMoves.Contains(index);
        // }
    }
}