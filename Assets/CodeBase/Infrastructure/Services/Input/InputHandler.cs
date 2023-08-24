using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputHandler : IInputHandler
    {
        private readonly IBoardTilesContainer _boardTilesContainer;
        private readonly IUnitsComander _unitsComander;
        private readonly ITurnManager _turnManager;
        private readonly Camera _camera;

        public InputHandler(IInputServiceMediator inputServiceMediator,ITurnManager turnManager, IUnitsComander unitsComander,
            IBoardTilesContainer boardTilesContainer)
        {
            _camera = Camera.main;
            _unitsComander = unitsComander;
            _boardTilesContainer = boardTilesContainer;
            _turnManager = turnManager;
            
            inputServiceMediator.OnClickedOnBoard += ProcessClickOnBoard;
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