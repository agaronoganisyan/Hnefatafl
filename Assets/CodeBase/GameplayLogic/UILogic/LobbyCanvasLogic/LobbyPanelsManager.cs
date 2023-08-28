using System.Collections.Generic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class LobbyPanelsManager  : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _connectionStatusText;

        [SerializeField] private LobbyPanel _allPanels;
        
        private Dictionary<LobbyPanelType, LobbyPanel> _panelsDictionary = new Dictionary<LobbyPanelType, LobbyPanel>();

        
        public void Initialize()
        {
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnConnectionStatusChanged += SetConnectionStatus;
        }

        void SetConnectionStatus(string status)
        {
            _connectionStatusText.text += status;
        }

        public void JumpBack()
        {
            
        }
    }
}