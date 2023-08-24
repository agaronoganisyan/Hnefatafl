using System.Threading.Tasks;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public class BoardHighlight : MonoBehaviour,IBoardHighlight
    {
        private IAssetsProvider _assetsProvider;
        
        [SerializeField] Color _currentTileColor;
        [SerializeField] Color _availableTileColor;
        
        ITileHighlight[,] _highlights;

        int _boardSize;

        private string _tileHighlightAddress = "TileHighlight"; 
        
        public void Initialize(IRuleManagerMediator ruleManagerMediator,IAssetsProvider assetsProvider,
            IUnitsPathCalculatorsManagerMediator unitsPathCalculatorsManagerMediator, IUnitsComanderMediator unitsComanderMediator)
        {
            _assetsProvider = assetsProvider;
            
            unitsPathCalculatorsManagerMediator.OnPathCalculated += EnableHighlight;
            unitsComanderMediator.OnUnitUnselected += DisableHighlight;

            ruleManagerMediator.OnGameRestarted += Restart;
        }

        void Restart()
        {
            DisableHighlight();
        }

        public async Task GenerateBoardHighlight(int boardSize)
        {
            _boardSize = boardSize;
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
