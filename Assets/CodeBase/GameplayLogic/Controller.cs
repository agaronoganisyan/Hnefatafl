using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic
{
    public class Controller : MonoBehaviour, IService
    {
        UnitsManager _unitsManager;
        Tile _selectedTile;
        BattleUnit _selectedUnit;

        TeamType _currentTeamOfTurn;

        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _tileLayer;

        Ray _ray;
        RaycastHit _hit;

        bool _isLocked;

        public static event Action<BattleUnit> OnUnitSelected;
        public static event Action<Tile> OnUnitPlaced;
        public static event Action OnDisableHighlight;

        public static event Action<TeamType> OnCurrentTeamOfTurnChanged;

        public void Initialize(UnitsManager unitsManager)
        {
            _unitsManager = unitsManager;

            GameManager.OnBlackTeamWon += () => SetControlLockStatus(true);
            GameManager.OnWhiteTeamWon += () => SetControlLockStatus(true);
        }

        public void Prepare()
        {
            SetTeamOfTurn(TeamType.White);

            SetControlLockStatus(false);
        }

        private void Update()
        {
            if (_isLocked) return;

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
                            SwitchTeamOfTurn();
                            OnUnitPlaced?.Invoke(_selectedTile);
                        }

                        OnDisableHighlight?.Invoke();

                        _selectedUnit = null;
                    }
                    else
                    {
                        _selectedUnit = _unitsManager.GetUnitByIndex(_selectedTile.Index);

                        if (_selectedUnit == null) return;

                        if (_selectedUnit.TeamType != _currentTeamOfTurn)
                        {
                            _selectedUnit = null;
                            return;
                        }

                        OnUnitSelected?.Invoke(_selectedUnit);
                    }
                }
            }
        }

        void SwitchTeamOfTurn()
        {
            if (_currentTeamOfTurn == TeamType.White) SetTeamOfTurn(TeamType.Black);
            else SetTeamOfTurn(TeamType.White);
        }

        void SetTeamOfTurn(TeamType teamType)
        {
            _currentTeamOfTurn = teamType;

            OnCurrentTeamOfTurnChanged?.Invoke(_currentTeamOfTurn);
        }

        void SetControlLockStatus(bool status) => _isLocked = status;

        private void OnEnable()
        {
            GameManager.OnGameStarted += Prepare;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= Prepare;
        }
    }
}