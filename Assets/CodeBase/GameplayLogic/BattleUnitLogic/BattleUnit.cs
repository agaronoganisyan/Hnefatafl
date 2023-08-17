using UnityEngine;

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
        [SerializeField] protected TeamType _teamType;
        public TeamType TeamType => _teamType;

        [SerializeField] protected UnitType _unitType;
        public UnitType UnitType => _unitType;

        protected Vector2Int _index;
        public Vector2Int Index => _index;
        
        bool _isKilled;
        public bool IsKilled => _isKilled;
        
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
    }
}