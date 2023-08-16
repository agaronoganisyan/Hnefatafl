using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsComander : IUnitsComander
    {
        private ITurnManager _turnManager;
        private IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        private IUnitsMoveValidator _unitsMoveValidator;
        private IUnitsStateContainer _unitsStateContainer;
        
        public UnitsComander(ITurnManager turnManager,IUnitsStateContainer unitsStateContainer, IUnitsPathCalculatorsManager unitsPathCalculatorsManager,
            IUnitsMoveValidator unitsMoveValidator)
        {
            _turnManager = turnManager;
            _unitsStateContainer = unitsStateContainer;
            _unitsPathCalculatorsManager = unitsPathCalculatorsManager;
            _unitsMoveValidator = unitsMoveValidator;
        }

        public void SelectUnit(BattleUnit unit)
        {
            _turnManager.SelectUnit(unit,_unitsPathCalculatorsManager.CalculatePath(unit));
        }

        public void MoveUnit(Vector2Int newIndex)
        {
            if (_unitsMoveValidator.IsUnitCanMove(_turnManager.SelectedUnitPath, newIndex))
            {
                _unitsStateContainer.RelocateUnit(_turnManager.SelectedUnit,newIndex);
            }
            else
            {
                
            }
        }
    }
}