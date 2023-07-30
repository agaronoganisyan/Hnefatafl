using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public enum BattleUnitType
    {
        None,
        King,
        Defender,
        Attacker
    }

    public abstract class BattleUnit : MonoBehaviour
    {
        [SerializeField] BattleUnitType _type;
        public BattleUnitType Type => _type;

        Vector2Int _index;
        public Vector2Int Index => _index;

        List<Vector2Int> _availableMoves = new List<Vector2Int>();
        public List<Vector2Int> AvailableMoves => _availableMoves;

        public void Initialize(Vector2Int index)
        {
            SetPosition(index);
        }

        public void SetPosition(Vector2Int index)
        {
            transform.position = new Vector3(index.x,0, index.y);
            _index = new Vector2Int(index.x, index.y);
        }

        public void CalculateAvailableMoves(Board board, UnitsManager unitsManager)
        {
            _availableMoves.Clear();

            //Down
            for (int i = _index.y - 1; i >= 0; i--)
            {
                if (!board.IsIndexAvailableToMove(new Vector2Int(_index.x, i), _type) || unitsManager.IsThereUnit(new Vector2Int(_index.x, i))) break;
                else _availableMoves.Add(new Vector2Int(_index.x, i));
            }

            //Up
            for (int i = _index.y + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                if (!board.IsIndexAvailableToMove(new Vector2Int(_index.x, i), _type) || unitsManager.IsThereUnit(new Vector2Int(_index.x, i))) break;
                else _availableMoves.Add(new Vector2Int(_index.x, i));
            }

            //Left
            for (int i = _index.x - 1; i >= 0; i--)
            {
                if (!board.IsIndexAvailableToMove(new Vector2Int(i, _index.y), _type) || unitsManager.IsThereUnit(new Vector2Int(i, _index.y))) break;
                else _availableMoves.Add(new Vector2Int(i, _index.y));
            }

            //Right
            for (int i = _index.x + 1; i < ConstValues.BOARD_SIZE; i++)
            {
                if (!board.IsIndexAvailableToMove(new Vector2Int(i, _index.y), _type) || unitsManager.IsThereUnit(new Vector2Int(i, _index.y))) break;
                else _availableMoves.Add(new Vector2Int(i, _index.y));
            }
        }

        public bool IsThisIndexAvailableToMove(Vector2Int index)
        {
            return _availableMoves.Contains(index) ? true : false;
        }
    }
}