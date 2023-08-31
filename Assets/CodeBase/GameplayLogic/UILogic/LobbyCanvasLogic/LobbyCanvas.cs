using CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class LobbyCanvas : UICanvas, ILobbyCanvas
    {
        [SerializeField] LobbyPanelsManager _lobbyPanelsManager;
        
        public void Initialize()
        {
            _lobbyPanelsManager.Initialize(this);
        }

        public void ClosePanel()
        {
            base.Close();
        }
    }
}