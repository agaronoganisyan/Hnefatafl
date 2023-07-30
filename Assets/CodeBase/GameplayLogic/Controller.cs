using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;

namespace CodeBase.GameplayLogic
{
    public class Controller : MonoBehaviour, IService
    {
        Board _board;
        UnitsManager _unitsManager;

        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _tileLayer;

        Ray _ray;
        RaycastHit _hit;

        Tile _selectedTile;
        BattleUnit _selectedUnit;

        public static event Action<BattleUnit> OnUnitSelected;
        public static event Action<Tile> OnUnitPlaced;
        public static event Action<Tile> OnDisableHighlight;

        public void Initialize(Board board,UnitsManager unitsManager)
        {
            _board = board;
            _unitsManager = unitsManager;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, 100, _tileLayer))
                {
                    _selectedTile = _hit.transform.GetComponent<Tile>();

                    if (_selectedUnit != null)
                    {
                        if (_selectedUnit.IsThisIndexAvailableToMove(_selectedTile.Index))
                        {
                            OnUnitPlaced?.Invoke(_selectedTile);
                        }
                        else
                        {
                            OnDisableHighlight?.Invoke(_selectedTile);
                        }

                        _selectedUnit = null;
                    }
                    else
                    {
                        _selectedUnit = _unitsManager.GetUnitByIndex(_selectedTile.Index);

                        if (_selectedUnit != null)
                        {
                            OnUnitSelected?.Invoke(_selectedUnit);
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}