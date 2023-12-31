﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CustomPoolLogic
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;

        public CustomPool(T prefab)
        {
            _prefab = prefab;
            _objects = new List<T>();
        }

        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void Expand(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var obj = Create();
                obj.gameObject.SetActive(false);
            }
        }

        public void DisableAllObjects()
        {
            for (int i=0;i<_objects.Count;i++) _objects[i].gameObject.SetActive(false);
        }

        private T Create()
        {
            var obj = Object.Instantiate(_prefab);
            _objects.Add(obj);
            return obj;
        }
    }
}