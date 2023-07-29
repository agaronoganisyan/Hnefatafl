using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ServiceLocatorLogic
{
    public static class ServiceLocator 
    {
        private static readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

        public static void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogWarning($"A service of type is already registered.");
                return;
            }

            _services.Add(key, service);
        }

        public static void Unregister<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogWarning($"No service of type registered.");
                return;
            }

            _services.Remove(key);
        }

        public static T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"No service of type registered.");
                return default;
            }

            return (T)_services[key];
        }
    }
}