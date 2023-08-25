using System.Threading.Tasks;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic;
using CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameFactoryLogic
{
    public class GameInfrastructureFactory : IGameInfrastructureFactory
    {
        private IAssetsProvider _assetsProvider;
        
        string _boardAddress = "Board";
        string _boardHighlightAddress = "BoardHighlight";
        string _gameplayCanvasAddress = "GameplayCanvas";
        string _debriefingCanvasAddress = "DebriefingCanvas";

        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }
        
        public async Task CreateBoard()
        {
            GameObject boardPrefab = await _assetsProvider.Load<GameObject>(_boardAddress);
            IBoard board = Object.Instantiate(boardPrefab).GetComponent<Board>();
            board.Initialize();
            await board.GenerateBoard();
        }

        public async Task CreateBoardHighlight()
        {
            GameObject boardHighlightPrefab = await _assetsProvider.Load<GameObject>(_boardHighlightAddress);
            IBoardHighlight boardHighlight = Object.Instantiate(boardHighlightPrefab).GetComponent<BoardHighlight>();
            boardHighlight.Initialize();
            await boardHighlight.GenerateBoardHighlight();
        }

        public async Task CreateGameplayCanvas()
        {
            GameObject gameplayCanvasPrefab = await _assetsProvider.Load<GameObject>(_gameplayCanvasAddress); 
            IGameplayCanvas gameplayCanvas = Object.Instantiate(gameplayCanvasPrefab).GetComponent<GameplayCanvas>();
            gameplayCanvas.Initialize();
        }

        public async Task CreateDebriefingCanvas()
        {
            GameObject debriefingCanvasPrefab = await _assetsProvider.Load<GameObject>(_debriefingCanvasAddress);
            IDebriefingCanvas debriefingCanvas = Object.Instantiate(debriefingCanvasPrefab).GetComponent<DebriefingCanvas>();
            debriefingCanvas.Initialize();
        }
    }
}