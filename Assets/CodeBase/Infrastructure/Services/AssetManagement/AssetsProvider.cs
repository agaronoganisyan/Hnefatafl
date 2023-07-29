using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public static class AssetsProvider 
    {
        private static readonly Dictionary<string, Component> _cachedAssets = new Dictionary<string, Component>();

        public static T GetCachedAsset<T>(string path) where T : Component
        {
            if (_cachedAssets.ContainsKey(path))
            {
                return _cachedAssets[path] as T;
            }
            else
            {
                T resource = Resources.Load<T>(path);
                _cachedAssets.Add(path, resource);
                return resource;
            }
        }
    }
}