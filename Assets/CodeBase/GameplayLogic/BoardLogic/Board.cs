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
    public class Board : MonoBehaviour, IService
    {
        Tile _intsTile;

        Tile[,] _tiles;

        Vector3 _thronPos;
        Vector3 _upperLeftShelterPos;
        Vector3 _upperRightShelterPos;
        Vector3 _lowerLeftShelterPos;
        Vector3 _lowerRightShelterPos;

        int _boardSize;

        public void Initialize()
        {
            _boardSize = ConstValues.BOARD_SIZE;
            GenerateBoard();
        }

        void GenerateBoard()
        {
            _tiles = new Tile[_boardSize, _boardSize];

            float boardSizeHalf = (float)_boardSize / 2 - 0.5f;
            _thronPos = new Vector3(boardSizeHalf, 0, boardSizeHalf);
            _upperLeftShelterPos = new Vector3(0,0,0);
            _upperRightShelterPos = new Vector3(0,0, _boardSize-1);
            _lowerLeftShelterPos = new Vector3(_boardSize-1, 0,0);
            _lowerRightShelterPos = new Vector3(_boardSize-1, 0, _boardSize-1);

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    Vector3 pos = new Vector3(x, 0, y);
                    InstantiateTile(GetTileTypeByPos(pos), pos);
                }
            }
        }

        void InstantiateTile(TileType tileType, Vector3 pos)
        {
            _intsTile = Instantiate(AssetsProvider.GetCachedAsset<Tile>(AssetsPath.PathToTile(tileType)), this.transform);
            _intsTile.Initialize(pos);
            _tiles[(int)pos.x, (int)pos.z] = _intsTile;
        }

        TileType GetTileTypeByPos(Vector3 pos)
        {
            if ((pos == _upperLeftShelterPos) || (pos == _upperRightShelterPos) || (pos == _lowerLeftShelterPos) || (pos == _lowerRightShelterPos)) return TileType.Shelter;
            else if (pos == _thronPos) return TileType.Thron;
            else return TileType.Regular;
        }

        public bool IsIndexAvailableToMove(Vector2Int index, BattleUnitType moveUnitType)
        {
            if (moveUnitType == BattleUnitType.King)
            {
                return _tiles[index.x, index.y] != null ? true : false;
            }
            else
            {
                return (_tiles[index.x, index.y] != null && _tiles[index.x, index.y].Type != TileType.Shelter) ? true : false;
            }
        }
    }
}