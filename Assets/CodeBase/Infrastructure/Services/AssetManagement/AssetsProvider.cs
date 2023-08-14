using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public static class AssetsProvider 
    {
        private static readonly Dictionary<string, Component> CachedAssets = new Dictionary<string, Component>();

        public static T GetCachedAsset<T>(string path) where T : Component
        {
            if (CachedAssets.TryGetValue(path, out var asset))
            {
                return asset as T;
            }
            else
            {
                T resource = Resources.Load<T>(path);
                CachedAssets.Add(path, resource);
                return resource;
            }
        }
    }
}