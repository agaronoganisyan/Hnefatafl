using UnityEngine;
using UnityEngine.UI;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.RoomLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingPanel : MonoBehaviour
    {
        private IGameRoomHandler _gameRoomHandler;
        
        [SerializeField] Image _panel;

        [SerializeField] Color _whitePanelColor;
        [SerializeField] Color _blackPanelColor;

        public void Initialize()
        {
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
        }

        public void Open(TeamType type)
        {
            Prepare(type);
        }

        void Prepare(TeamType type)
        {
            if (type == TeamType.White) _panel.color = _whitePanelColor;
            else if (type == TeamType.Black) _panel.color = _blackPanelColor;
        }

        public void LobbyButton()
        {
            _gameRoomHandler.Quit();
        }
    }
}