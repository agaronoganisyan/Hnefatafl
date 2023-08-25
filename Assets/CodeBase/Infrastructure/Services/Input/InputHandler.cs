using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputHandler : IInputHandler
    {
        private IBoardTilesContainer _boardTilesContainer;
        private IUnitsComander _unitsComander;
        private ITurnManager _turnManager;
        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
            _unitsComander = ServiceLocator.Get<IUnitsComander>();
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _turnManager = ServiceLocator.Get<ITurnManager>();
            
            ServiceLocator.Get<IInputServiceMediator>().OnClickedOnBoard += ProcessClickOnBoard;
        }
        
        public void ProcessClickOnBoard(Vector2 mousePosition)
        {
            if (!_boardTilesContainer.IsWorldPosOnBoard(_camera.ScreenToWorldPoint(mousePosition))) return;
            
            Vector2Int index = _boardTilesContainer.GetIndexByWorldPos(_camera.ScreenToWorldPoint(mousePosition));
            
            if (_turnManager.SelectedUnit != null)
            {
                _unitsComander.MoveUnit(index);
            }
            else
            {
                _unitsComander.SelectUnit(index);
            }
        }
    }
}