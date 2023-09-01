using TMPro;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerPanelLogic
{
    public class CreateNewRoomPanel : MonoBehaviour
    {
        private MultiplayerPanel _multiplayerPanel;
        
        [SerializeField] private TMP_InputField _inputField;

        public void Initialize(MultiplayerPanel multiplayerPanel)
        {
            _multiplayerPanel = multiplayerPanel;
        }

        public void CreateRoomButton()
        {
            _multiplayerPanel.CreateRoom(_inputField.text);
        }
    }
}