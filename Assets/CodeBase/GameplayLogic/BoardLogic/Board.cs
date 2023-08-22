using System.Threading.Tasks;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class Board : MonoBehaviour, IBoard
    {
        private IBoardTilesContainer _boardTilesContainer;
        private IAssetsProvider _assetsProvider;
        
        string TileAddress(TileType type) => "Tile_" + type;
        
        public async Task GenerateBoard(int boardSize, IBoardTilesContainer boardTilesContainer,
            IAssetsProvider assetsProvider)
        {
            _boardTilesContainer = boardTilesContainer;
            _assetsProvider = assetsProvider;
            
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    Vector2Int index = new Vector2Int(x,  y);
                    await InstantiateTile(_boardTilesContainer.GetTileTypeByIndex(index), index);
                }
            }
        }

        async Task InstantiateTile(TileType tileType, Vector2Int index)
        {
            GameObject tilePrefab = await _assetsProvider.Load<GameObject>(TileAddress(tileType));
            Tile intsTile = Instantiate(tilePrefab, transform).GetComponent<Tile>();
            intsTile.Initialize(index);
        }
    }
}