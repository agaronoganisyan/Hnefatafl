using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public abstract class RoomListEntry : MonoBehaviour
    {
        private INetworkManager _networkManager;
        
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
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _rectTransform.SetParent(parent);
            _rectTransform.localScale = Vector3.one;
        }

        public void PrepareContent(string roomName, byte currentPlayers, byte maxPlayers)
        {
            _roomName = roomName;

            _roomNameText.text = _roomName;
            _roomPlayersText.text = $"{currentPlayers}/{maxPlayers}";

            _button.interactable = currentPlayers < maxPlayers;
        }
        
        public void JoinButton()
        {
            _networkManager.JoinPrescribedRoom(_roomName);
        }
    }
}