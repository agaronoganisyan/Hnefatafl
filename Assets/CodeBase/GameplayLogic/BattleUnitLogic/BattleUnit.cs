using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        int _currentX;
        int _currentY;

        public void Initialize(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}