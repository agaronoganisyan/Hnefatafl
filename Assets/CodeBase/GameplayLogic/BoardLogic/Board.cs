using System.Threading.Tasks;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class Board : MonoBehaviour, IBoard
    {
        private IBoardTilesContainer _boardTilesContainer;
        private IAssetsProvider _assetsProvider;

        private int _boardSize;
        
        string TileAddress(TileType type) => "Tile_" + type;

        public void Initialize()
        {
            GameModeStaticData currentModeData =
                ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);

            _boardSize = currentModeData.BoardSize;
            
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async Task GenerateBoard()
        {
            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
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