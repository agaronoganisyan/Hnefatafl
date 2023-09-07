using System.Threading.Tasks;
using UnityEngine;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class BoardHighlight : MonoBehaviour , IBoardHighlight, IGameplayModeChangingObserver
    {
        private IAssetsProvider _assetsProvider;
        private IUnitsPathCalculatorsManager _calculatorsManager;
        private IUnitsCommander _unitsCommander;
        private IGameRoomHandler _gameRoomHandler;
        
        [SerializeField] Color _currentTileColor;
        [SerializeField] Color _availableTileColor;
        
        ITileHighlight[,] _highlights;

        int _boardSize;

        private string _tileHighlightAddress = "TileHighlight"; 
        
        public void Initialize()
        {
            GameModeStaticData currentModeData =
                ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);

            _boardSize = currentModeData.BoardSize;
            
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();

            _calculatorsManager = ServiceLocator.Get<IUnitsPathCalculatorsManager>();
            _unitsCommander= ServiceLocator.Get<IUnitsCommander>();
            _gameRoomHandler= ServiceLocator.Get<IGameRoomHandler>();

            _calculatorsManager.Mediator.OnPathCalculated += EnableHighlight;
            _unitsCommander.Mediator.OnUnitUnselected += DisableHighlight;
            _gameRoomHandler.Mediator.OnQuitRoom += Restart;

            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
            _calculatorsManager.Mediator.OnPathCalculated -= EnableHighlight;
            _unitsCommander.Mediator.OnUnitUnselected -= DisableHighlight;
            _gameRoomHandler.Mediator.OnQuitRoom -= Restart;
            
            _calculatorsManager = ServiceLocator.Get<IUnitsPathCalculatorsManager>();
            _unitsCommander= ServiceLocator.Get<IUnitsCommander>();
            _gameRoomHandler= ServiceLocator.Get<IGameRoomHandler>();

            _calculatorsManager.Mediator.OnPathCalculated += EnableHighlight;
            _unitsCommander.Mediator.OnUnitUnselected += DisableHighlight;
            _gameRoomHandler.Mediator.OnQuitRoom += Restart;
        }
        
        void Restart()
        {
            DisableHighlight();
        }

        public async Task GenerateBoardHighlight()
        {
            _highlights = new ITileHighlight[_boardSize, _boardSize];

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
                {
                    Vector3 pos = new Vector3(x, 0, y);
                    await InstantiateTile(pos);
                }
            }
        }

        async Task InstantiateTile(Vector3 pos)   
        {
            GameObject highlightPrefab = await _assetsProvider.Load<GameObject>(_tileHighlightAddress);

            ITileHighlight intsHighlight = Instantiate(highlightPrefab, transform).GetComponent<TileHighlight>();
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
    }
}
