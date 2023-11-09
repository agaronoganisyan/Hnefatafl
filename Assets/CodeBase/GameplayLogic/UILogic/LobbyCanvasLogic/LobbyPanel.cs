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
        
        public virtual Task Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            _lobbyPanelsManager = lobbyPanelsManager;
            return Task.CompletedTask;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        public void JumpBack()
        {
            _lobbyPanelsManager.JumpBack();
        }
    }
}