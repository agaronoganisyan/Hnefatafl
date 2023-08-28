using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public enum LobbyPanelType
    {
        None,
        PlaymodeSelection,
        Connection
    }

    public abstract class LobbyPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private LobbyPanelsManager _lobbyPanelsManager;
        
        public virtual void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            _lobbyPanelsManager = lobbyPanelsManager;
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 1;
        }
        
        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
        }
        
        public virtual void JumpBack()
        {
            _lobbyPanelsManager.JumpBack();
        }
    }
}