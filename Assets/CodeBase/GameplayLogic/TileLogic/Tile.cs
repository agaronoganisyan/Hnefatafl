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

        Vector2Int _index;
        public Vector2Int Index => _index;

        public void Initialize(Vector3 pos)
        {
            transform.position = pos;
            _index = new Vector2Int((int)pos.x, (int)pos.z);
        }
    }
}
