using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputHandler : IInputHandler
    {
        private IBoardTilesContainer _boardTilesContainer;
        private IUnitsCommander _unitsCommander;
        protected ITurnManager _turnManager;
        private Camera _camera;

        public virtual void Initialize()
        {
            _camera = Camera.main;
            _unitsCommander = ServiceLocator.Get<IUnitsCommander>();
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _turnManager = ServiceLocator.Get<ITurnManager>();
            
            ServiceLocator.Get<IInputService>().InputServiceMediator.OnClickedOnBoard += ProcessClickOnBoard;
        }
        
        public void ProcessClickOnBoard(Vector2 mousePosition)
        {
            if (!IsCanProcessClick()) return;
            
            if (!_boardTilesContainer.IsWorldPosOnBoard(_camera.ScreenToWorldPoint(mousePosition))) return;
            
            Vector2Int index = _boardTilesContainer.GetIndexByWorldPos(_camera.ScreenToWorldPoint(mousePosition));
            
            if (_turnManager.SelectedUnit != null)
            {
                _unitsCommander.MoveUnit(index);
            }
            else
            {
                _unitsCommander.SelectUnit(index);
            }
        }

        protected abstract bool IsCanProcessClick();
    }
}