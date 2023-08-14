using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "ModeStaticData", menuName = "StaticData/GameMode")]
    public class GameModeStaticData : ScriptableObject
    {
        public int BoardSize;

        public int WhiteWarriorsAmount;
        public int BlackWarriorsAmount;
    }
}
