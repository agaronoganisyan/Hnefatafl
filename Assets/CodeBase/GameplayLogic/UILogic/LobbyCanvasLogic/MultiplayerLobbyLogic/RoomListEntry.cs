using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.RoomManagerLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public abstract class RoomListEntry : MonoBehaviour
    {
        private INetworkRoomManager _networkRoomManager;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _roomNameText;
        [SerializeField] private TextMeshProUGUI _roomPlayersText;
        [SerializeField] private Button _button;

        private bool _isInitialize;
        
        private string _roomName;
        
        public void Initialize(Transform parent)
        {
            if(_isInitialize) return;
            
            _isInitialize = true;
            _networkRoomManager = ServiceLocator.Get<INetworkRoomManager>();
            _rectTransform.SetParent(parent);
            _rectTransform.localScale = Vector3.one;
        }

        public void PrepareContent(string roomName, byte currentPlayers, byte maxPlayers)
        {
            _roomName = roomName;

            _roomNameText.text = _roomName;
            _roomPlayersText.text = $"{currentPlayers}/{maxPlayers}";

            _button.gameObject.SetActive(!(currentPlayers >= maxPlayers));
            
            gameObject.SetActive(true);
        }
        
        public void JoinButton()
        {
            _networkRoomManager.JoinPrescribedRoom(_roomName);
        }
    }
}