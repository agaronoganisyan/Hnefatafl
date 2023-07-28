using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic.TileLogic
{
    public enum TileType
    {
        None,
        Regular,
        Thron,
        Shelter
    }

    public class Tile : MonoBehaviour
    {
        [SerializeField] TileType _type;
        public TileType Type => _type;

        public void Initialize(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}
