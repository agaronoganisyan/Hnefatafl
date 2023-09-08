using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using TMPro;
using UnityEngine;

namespace CodeBase.NetworkLogic.UI
{
    public class NetworkLoadingPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _connectionStatusText;
        
        public void Initialize()
        {
            ServiceLocator.Get<INetworkManager>().Mediator.OnConnectionStatusChanged += SetConnectionStatus;
        }
        
        void SetConnectionStatus(string status)
        {
            _connectionStatusText.text = status;
        }
    }
}