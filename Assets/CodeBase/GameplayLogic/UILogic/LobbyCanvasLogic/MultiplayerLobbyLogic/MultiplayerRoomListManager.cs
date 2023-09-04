using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public class MultiplayerRoomListManager : MonoBehaviour
    {
        private const string StandardRoomListEntryAddress = "StandardRoomListEntry";
        
        private IAssetsProvider _assetsProvider;
        
        [SerializeField] private RectTransform _listContent;

        private CustomPool<RoomListEntry> _roomListStandardEntriesPool;
        public async Task Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
            
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnRoomListUpdated += UpdateRoomList;

            await InitializePool();
        }

        void UpdateRoomList(List<RoomInfo> roomList)
        {
            HideRoomList();
            
            for (int i=0; i<roomList.Count; i++)
            {
                RoomInfo info = roomList[i];
                
                RoomListEntry entry = _roomListStandardEntriesPool.Get();
                entry.Initialize(_listContent);
                entry.PrepareContent(info.Name, (byte)info.PlayerCount, (byte)info.MaxPlayers);
            }
        }

        void HideRoomList()
        {
            _roomListStandardEntriesPool.DisableAllObjects();
        }

        async Task InitializePool()
        {
            GameObject roomListEntryPrefab = await _assetsProvider.Load<GameObject>(StandardRoomListEntryAddress);
            _roomListStandardEntriesPool = new CustomPool<RoomListEntry>(roomListEntryPrefab.GetComponent<RoomListEntry>());
        }
    }
}