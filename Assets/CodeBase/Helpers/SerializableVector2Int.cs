using System;
using UnityEngine;

namespace CodeBase.Helpers
{
    [System.Serializable]
    public static class SerializableVector2Int
    {
        public static object DeserializeVector2Int(byte[] data)
        {
            Vector2Int result = new Vector2Int();

            result.x = BitConverter.ToInt32(data, 0);
            result.y = BitConverter.ToInt32(data, 4);

            return result;
        }

        public static byte[] SerializeVector2Int(object obj)
        {
            Vector2Int vector = (Vector2Int)obj;
            byte[] result = new byte[8];
            
            BitConverter.GetBytes(vector.x).CopyTo(result,0);
            BitConverter.GetBytes(vector.y).CopyTo(result,4);

            return result;
        }
    }
}