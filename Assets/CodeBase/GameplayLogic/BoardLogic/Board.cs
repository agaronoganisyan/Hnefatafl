using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class Board : MonoBehaviour, IBoard
    {
        private IBoardTilesContainer _boardTilesContainer;
        
        public void GenerateBoard(int boardSize, IBoardTilesContainer boardTilesContainer)
        {
            _boardTilesContainer = boardTilesContainer;
            
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    Vector2Int index = new Vector2Int(x,  y);
                    InstantiateTile(_boardTilesContainer.GetTileTypeByIndex(index), index);
                }
            }
        }

        void InstantiateTile(TileType tileType, Vector2Int index)
        {
            Tile intsTile = Instantiate(AssetsProvider.GetCachedAsset<Tile>(AssetsPath.PathToTile(tileType)), transform);
            intsTile.Initialize(index);
        }
    }
}