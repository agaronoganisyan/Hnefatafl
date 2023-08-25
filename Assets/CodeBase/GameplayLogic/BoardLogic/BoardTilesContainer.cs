using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class BoardTilesContainer : IBoardTilesContainer
    {
    Vector2Int _thronIndex;
    Vector2Int _upperLeftShelterIndex;
    Vector2Int _upperRightShelterIndex;
    Vector2Int _lowerLeftShelterIndex;
    Vector2Int _lowerRightShelterIndex;

    int _boardSize;
    
    public void Initialize()
    {
        GameModeStaticData currentModeData =
            ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);
        
        _boardSize = currentModeData.BoardSize;
    }
    public void GenerateBoard()
    {
        int boardSizeHalf = (int)((float)_boardSize / 2 - 0.5f);
        _thronIndex = new Vector2Int(boardSizeHalf, boardSizeHalf);
        _upperLeftShelterIndex = new Vector2Int(0, 0);
        _upperRightShelterIndex = new Vector2Int(0, _boardSize - 1);
        _lowerLeftShelterIndex = new Vector2Int(_boardSize - 1, 0);
        _lowerRightShelterIndex = new Vector2Int(_boardSize - 1, _boardSize - 1);
    }
    
    public TileType GetTileTypeByIndex(Vector2Int index)
    {
        if (index == _upperLeftShelterIndex || index == _upperRightShelterIndex ||
            index == _lowerLeftShelterIndex || index == _lowerRightShelterIndex) return TileType.Shelter;
        
        if (index == _thronIndex) return TileType.Thron;
        
        return TileType.Regular;
    }

    public bool IsIndexOnBoard(Vector2Int index)
    {
        return index.x >= 0 && index.y >= 0 && index.x < _boardSize && index.y < _boardSize;
    }

    public bool IsWorldPosOnBoard(Vector3 worldPos)
    {
        return IsIndexOnBoard(GetIndexByWorldPos(worldPos));
    }

    public Vector2Int GetIndexByWorldPos(Vector3 worldPos)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
    }
    }
}