using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;
using Tile = UnityEngine.WSA.Tile;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardTilesContainer
    {
        void GenerateBoard(int boardSize);
        TileType GetTileTypeByIndex(Vector2Int index);
        bool IsIndexOnBoard(Vector2Int index);
        bool IsWorldPosOnBoard(Vector3 worldPos);
        Vector2Int GetIndexByWorldPos(Vector3 worldPos);
    }
}