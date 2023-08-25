using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssets = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        
        public async Task<T> Load<T>(string address) where T: class
        {
            if (_cachedAssets.TryGetValue(address, out AsyncOperationHandle cachedHandle))
                return cachedHandle.Result as T;
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            handle.Completed += operationHandle =>
            {
                _cachedAssets[address] = operationHandle;
            };

            AddHandle(address, handle);

            return await handle.Task;
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles  in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
                Addressables.Release(handle);
            
            _cachedAssets.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string path, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(path, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[path] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}