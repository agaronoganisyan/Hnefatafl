using CodeBase.GameplayLogic.BoardLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public abstract class UnitPathCalculator
    {
        protected IBoardTilesContainer _boardTilesContainer;
        protected IUnitsStateContainer _unitsStateContainer;

        private readonly int _boardSize;
        
        public UnitPathCalculator(IBoardTilesContainer boardTilesContainer,IUnitsStateContainer unitsStateContainer, int boardSize)
        {
            _boardTilesContainer = boardTilesContainer;
            _unitsStateContainer = unitsStateContainer;

            _boardSize = boardSize;
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
            for (int i = currentIndex.y + 1; i < _boardSize; i++)
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
            for (int i = currentIndex.x + 1; i < _boardSize; i++)
            {
                Vector2Int index = new Vector2Int(i, currentIndex.y);

                if (IsThereProblemWithIndex(index)) break;
                else path.AddAvailableMove(index);
            }

            return path;
        }
        
        protected abstract bool IsThereProblemWithIndex(Vector2Int index);

        public bool IsThereAvailableMoves(Vector2Int currentIndex)
        {
            IUnitPath unitPath = CalculatePaths(currentIndex);
            return unitPath.AvailableMoves.Count>0;
        }
        
    }
}