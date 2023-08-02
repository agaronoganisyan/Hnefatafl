using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class BoardHighlight : MonoBehaviour,IService
    {
        [SerializeField] Color _currentTileColor;
        [SerializeField] Color _availableTileColor;

        BattleUnit _selectedUnit;

        TileHighlight[,] _highlights;

        int _boardSize;

        public void Initialize()
        {
            _boardSize = ConstValues.BOARD_SIZE;
            GenerateBoardHighlight();
        }

        public void Restart()
        {
            _selectedUnit = null;
            DisableAllHighlights();
        }

        void GenerateBoardHighlight()
        {
            _highlights = new TileHighlight[_boardSize, _boardSize];

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    Vector3 pos = new Vector3(x, 0, y);
                    InstantiateTile( pos);
                }
            }
        }

        void InstantiateTile(Vector3 pos)
        {
            TileHighlight intsHighlight = Instantiate(AssetsProvider.GetCachedAsset<TileHighlight>(AssetsPath.PathToTileHighlight), transform);
            intsHighlight.Initialize(pos,_currentTileColor, _availableTileColor);
            _highlights[(int)pos.x, (int)pos.z] = intsHighlight;
        }

        void EnableHighlight(BattleUnit currentUnit)
        {
            _selectedUnit = currentUnit;

            _highlights[_selectedUnit.IndexBeforeMove.x, _selectedUnit.IndexBeforeMove.y].Show(ShowingType.Current);

            for (int i = 0; i < _selectedUnit.AvailableMoves.Count; i++)
            {
                _highlights[_selectedUnit.AvailableMoves[i].x, _selectedUnit.AvailableMoves[i].y].Show(ShowingType.Available);
            }
        }

        void DisableHighlight()
        {
            _highlights[_selectedUnit.IndexBeforeMove.x, _selectedUnit.IndexBeforeMove.y].Hide();

            for (int i = 0; i < _selectedUnit.AvailableMoves.Count; i++)
            {
                _highlights[_selectedUnit.AvailableMoves[i].x, _selectedUnit.AvailableMoves[i].y].Hide();
            }
        }

        void DisableAllHighlights()
        {
            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    _highlights[x, y].Hide();
                }
            }
        }

        private void OnEnable()
        {
            GameManager.OnGameRestarted += Restart;
            UnitsManager.OnSelectedUnitMovesCalculated += EnableHighlight;
            Controller.OnDisableHighlight += DisableHighlight;
        }

        private void OnDisable()
        {
            GameManager.OnGameRestarted -= Restart;
            UnitsManager.OnSelectedUnitMovesCalculated -= EnableHighlight;
            Controller.OnDisableHighlight -= DisableHighlight;
        }
    }
}
