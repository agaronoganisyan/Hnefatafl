using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerPanelLogic
{
    public class MultiplayerRoomListManager: MonoBehaviour
    {
        private IAssetsProvider _assetsProvider;
        
        [SerializeField] private RectTransform _listContent;

        private CustomPool<RoomListEntry> _roomListEntriesPool;
        
        private const string StandardRoomListEntryAddress = "StandardRoomListEntry";
        
        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }
        
        public async Task InitializePool()
        {
            GameObject roomListEntryPrefab = await _assetsProvider.Load<GameObject>(StandardRoomListEntryAddress);
            _roomListEntriesPool = new CustomPool<RoomListEntry>(roomListEntryPrefab.GetComponent<RoomListEntry>());
        }
    }
}