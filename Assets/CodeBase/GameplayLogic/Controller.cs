using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _tileLayer;
        [SerializeField] LayerMask _unitLayer;

        Ray _ray;
        RaycastHit _hit;

        bool _isUnitSelected;

        public static event Action<BattleUnit> OnUnitSelected;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (_isUnitSelected)
                {

                //    UnitsManager.istg

                    if (Physics.Raycast(_ray, out _hit, 100, _tileLayer))
                    {

                    }
                }
                else
                {
                    if (Physics.Raycast(_ray, out _hit, 100, _unitLayer))
                    {
                        _isUnitSelected = true;

                      //  OnUnitSelected?.Invoke(_hit.collider.tr);
                    }
                }
            }
        }
    }
}