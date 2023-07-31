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

        public void Initialize(Vector2Int index)
        {
            transform.position = new Vector3(index.x, 0, index.y);
            _index = new Vector2Int(index.x, index.y);
        }
    }
}
