using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public enum TeamType
    {
        None,
        White,
        Black
    }

    public enum UnitType
    {
        None,
        King,
        Warrior
    }

    public abstract class BattleUnit : MonoBehaviour
    {
        protected Board _board;
        protected UnitsManager _unitsManager;
        protected IUnitsStateContainer _unitsStateContainer;
        
        [SerializeField] protected TeamType _teamType;
        public TeamType TeamType => _teamType;

        [SerializeField] protected UnitType _unitType;
        public UnitType UnitType => _unitType;

        protected Vector2Int _index;
        public Vector2Int Index => _index;

        protected Vector2Int _indexBeforeMove;
        public Vector2Int IndexBeforeMove => _indexBeforeMove;

        bool _isKilled;
        public bool IsKilled => _isKilled;

        protected List<Vector2Int> _availableMoves = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> AvailableMoves => _availableMoves;



        public void Initialize(Board board , UnitsManager unitsManager)
        {
            _board = board;
            _unitsManager = unitsManager;
        }

        void SetPosition(Vector2Int index)
        {
            transform.position = new Vector3(index.x,0, index.y);
            _index = new Vector2Int(index.x, index.y);
        }

        public void PrepareUnit(Vector2Int index)
        {
            SetPosition(index);
            SetActiveStatus(true);
        }

        public void Kill()
        {
            SetActiveStatus(false);
        }

        public void SetActiveStatus(bool status)
        {
            _isKilled = !status;
            gameObject.SetActive(status);
        }

        public void CalculateAvailableMoves()
        {
            _availableMoves.Clear();

            _indexBeforeMove = _index;

            //Down
            for (int i = _index.y - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(_index.x, i);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Up
            for (int i = _index.y + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(_index.x, i);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Left
            for (int i = _index.x - 1; i >= 0; i--)
            {
                Vector2Int index = new Vector2Int(i, _index.y);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }

            //Right
            for (int i = _index.x + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                Vector2Int index = new Vector2Int(i, _index.y);

                if (IsThereProblemWithIndex(index)) break;
                else _availableMoves.Add(index);
            }
        }

        protected abstract bool IsThereProblemWithIndex(Vector2Int index);

        public bool IsThereAvailableMoves()
        {
            CalculateAvailableMoves();
            return _availableMoves.Count>0 ? true : false;
        }

        public bool IsThisIndexAvailableToMove(Vector2Int index)
        {
            return _availableMoves.Contains(index) ? true : false;
        }
    }
}