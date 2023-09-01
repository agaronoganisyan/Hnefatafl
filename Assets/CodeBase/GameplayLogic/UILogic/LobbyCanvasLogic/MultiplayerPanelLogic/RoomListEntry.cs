using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerPanelLogic
{
    public abstract class RoomListEntry : MonoBehaviour
    {
        private INetworkManager _networkManager;
        
        [SerializeField] private TextMeshProUGUI _roomNameText;
        [SerializeField] private TextMeshProUGUI _roomPlayersText;

        private string _roomName;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
        }

        public void Prepare(string name, byte currentPlayers, byte maxPlayers)
        {
            _roomName = name;

            _roomNameText.text = _roomName;
            _roomPlayersText.text = $"{currentPlayers}/{maxPlayers}";
        }
        
        public void JoinButton()
        {
            _networkManager.JoinPrescribedRoom(_roomName);
        }
    }
}