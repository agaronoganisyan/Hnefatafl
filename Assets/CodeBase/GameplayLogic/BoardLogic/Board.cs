using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class Board : MonoBehaviour, IBoard
    {
        Tile[,] _tiles;

        Vector2Int _thronIndex;
        Vector2Int _upperLeftShelterIndex;
        Vector2Int _upperRightShelterIndex;
        Vector2Int _lowerLeftShelterIndex;
        Vector2Int _lowerRightShelterIndex;

        int _boardSize;
        
        public void GenerateBoard(int boardSize)
        {
            _boardSize = boardSize;
            _tiles = new Tile[_boardSize, _boardSize];

            int boardSizeHalf = (int)((float)_boardSize / 2 - 0.5f);
            _thronIndex = new Vector2Int(boardSizeHalf, boardSizeHalf);
            _upperLeftShelterIndex = new Vector2Int(0,0);
            _upperRightShelterIndex = new Vector2Int(0, _boardSize-1);
            _lowerLeftShelterIndex = new Vector2Int(_boardSize-1, 0);
            _lowerRightShelterIndex = new Vector2Int(_boardSize-1,  _boardSize-1);

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    Vector2Int index = new Vector2Int(x,  y);
                    InstantiateTile(GetTileTypeByIndex(index), index);
                }
            }
        }

        void InstantiateTile(TileType tileType, Vector2Int index)
        {
            Tile intsTile = Instantiate(AssetsProvider.GetCachedAsset<Tile>(AssetsPath.PathToTile(tileType)), transform);
            intsTile.Initialize(index);
            _tiles[index.x, index.y] = intsTile;
        }

        public TileType GetTileTypeByIndex(Vector2Int index)
        {
            if ((index == _upperLeftShelterIndex) || (index == _upperRightShelterIndex) || (index == _lowerLeftShelterIndex) || (index == _lowerRightShelterIndex)) return TileType.Shelter;
            else if (index == _thronIndex) return TileType.Thron;
            else return TileType.Regular;
        }

        public bool IsIndexAvailableToMove(Vector2Int index)
        {
            return _tiles[index.x, index.y] != null ? true : false;
        }

        public bool IsIndexOnBoard(Vector2Int index)
        {
            return index.x < 0 || index.y < 0 || index.x >= _boardSize || index.y >= _boardSize ? false : true;
        }
    }
}