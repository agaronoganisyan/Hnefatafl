using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class BoardHighlight : MonoBehaviour,IBoardHighlight
    {
        [SerializeField] Color _currentTileColor;
        [SerializeField] Color _availableTileColor;
        
        TileHighlight[,] _highlights;

        int _boardSize;

        public void Initialize(IGameManager gameManager, IUnitsPathCalculatorsManager unitsPathCalculatorsManager, IUnitsComander unitsComander)
        {
            unitsPathCalculatorsManager.OnPathCalculated += EnableHighlight;
            unitsComander.OnUnitUnselected += DisableHighlight;

            gameManager.OnGameRestarted += Restart;
        }

        void Restart()
        {
            DisableHighlight();
        }

        public void GenerateBoardHighlight(int boardSize)
        {
            _boardSize = boardSize;
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

        void EnableHighlight(IUnitPath path)
        {
            _highlights[path.CurrentIndex.x, path.CurrentIndex.y].Show(ShowingType.Current);

            for (int i = 0; i < path.AvailableMoves.Count; i++)
            {
                _highlights[path.AvailableMoves[i].x, path.AvailableMoves[i].y].Show(ShowingType.Available);
            }
        }

        void DisableHighlight()
        {
            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    _highlights[x,y].Hide();
                }
            }
        }

        // private void OnEnable()
        // {
        //     
        //     
        //     
        //     
        //     GameManager.OnGameRestarted += Restart;
        //     //UnitsManager.OnSelectedUnitMovesCalculated += EnableHighlight;
        //     Controller.OnDisableHighlight += DisableHighlight;
        // }
        //
        // private void OnDisable()
        // {
        //     
        //     
        //     
        //     
        //     GameManager.OnGameRestarted -= Restart;
        //     //UnitsManager.OnSelectedUnitMovesCalculated -= EnableHighlight;
        //     Controller.OnDisableHighlight -= DisableHighlight;
        // }
    }
}
