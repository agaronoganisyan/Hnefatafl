using TMPro;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public class CreateNewRoomPanel : MonoBehaviour
    {
        private MultiplayerLobby _multiplayerLobby;
        
        [SerializeField] private TMP_InputField _inputField;

        public void Initialize(MultiplayerLobby multiplayerLobby)
        {
            _multiplayerLobby = multiplayerLobby;
        }

        public void CreateRoomButton()
        {
            if (_inputField.text.Length <= 0) return;
            _multiplayerLobby.CreateRoom(_inputField.text);
        }
        
         
    }
}