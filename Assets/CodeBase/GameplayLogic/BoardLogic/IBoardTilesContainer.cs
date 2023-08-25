using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardTilesContainer : IService
    {
        void GenerateBoard();
        TileType GetTileTypeByIndex(Vector2Int index);
        bool IsIndexOnBoard(Vector2Int index);
        bool IsWorldPosOnBoard(Vector3 worldPos);
        Vector2Int GetIndexByWorldPos(Vector3 worldPos);
    }
}