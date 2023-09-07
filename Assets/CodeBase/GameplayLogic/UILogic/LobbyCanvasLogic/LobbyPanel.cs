using UnityEngine;
using System.Threading.Tasks;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public enum LobbyPanelType
    {
        None,
        PlaymodeSelection,
        MultiplayerLobby,
        TeamSelection
    }

    public abstract class LobbyPanel : MonoBehaviour
    {
        public LobbyPanelType Type => _type;
        protected LobbyPanelType _type;

        protected LobbyPanelsManager _lobbyPanelsManager;

        [SerializeField] private CanvasGroup _canvasGroup;
        
        public virtual Task Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            _lobbyPanelsManager = lobbyPanelsManager;
            return Task.CompletedTask;
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        public void JumpBack()
        {
            _lobbyPanelsManager.JumpBack();
        }
    }
}