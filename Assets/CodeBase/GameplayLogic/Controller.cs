using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _tileLayer;

        Ray _ray;
        RaycastHit _hit;
        private void Update()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 100, _tileLayer))
            {

            }
        }
    }
}