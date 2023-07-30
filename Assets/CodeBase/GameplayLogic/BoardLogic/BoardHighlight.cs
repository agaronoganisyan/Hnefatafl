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

        TileHighlight _intsHighlight;
        BattleUnit _selectedUnit;

        TileHighlight[,] _highlights;

        int _boardSize;

        public void Initialize()
        {
            _boardSize = ConstValues.BOARD_SIZE;
            GenerateBoardHighlight();
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
            _intsHighlight = Instantiate(AssetsProvider.GetCachedAsset<TileHighlight>(AssetsPath.PathToTileHighlight), this.transform);
            _intsHighlight.Initialize(pos,_currentTileColor, _availableTileColor);
            _highlights[(int)pos.x, (int)pos.z] = _intsHighlight;
        }

        void EnableHighlight(BattleUnit currentUnit)
        {
            _selectedUnit = currentUnit;

            _highlights[_selectedUnit.Index.x, _selectedUnit.Index.y].Show(ShowingType.Current);

            for (int i = 0; i < _selectedUnit.AvailableMoves.Count; i++)
            {
                _highlights[_selectedUnit.AvailableMoves[i].x, _selectedUnit.AvailableMoves[i].y].Show(ShowingType.Available);
            }
        }

        void DisableHighlight(Tile finalTile)
        {
            _highlights[_selectedUnit.Index.x, _selectedUnit.Index.y].Hide();

            for (int i = 0; i < _selectedUnit.AvailableMoves.Count; i++)
            {
                _highlights[_selectedUnit.AvailableMoves[i].x, _selectedUnit.AvailableMoves[i].y].Hide();
            }
        }

        private void OnEnable()
        {
            UnitsManager.OnSelectedUnitMovesCalculated += EnableHighlight;
            Controller.OnUnitPlaced += DisableHighlight;
            Controller.OnDisableHighlight += DisableHighlight;
        }

        private void OnDisable()
        {
            UnitsManager.OnSelectedUnitMovesCalculated -= EnableHighlight;
            Controller.OnUnitPlaced -= DisableHighlight;
            Controller.OnDisableHighlight -= DisableHighlight;
        }
    }
}
