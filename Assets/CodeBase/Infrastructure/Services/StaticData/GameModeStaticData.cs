using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "ModeStaticData", menuName = "StaticData/GameMode")]
    public class GameModeStaticData : ScriptableObject
    {
        public int BoardSize => _boardSize;
        [SerializeField] private int _boardSize;
        
        public int WhiteWarriorsAmount => _whiteWarriorsAmount;
        [SerializeField] private int _whiteWarriorsAmount;
        
        public int BlackWarriorsAmount => _blackWarriorsAmount;
        [SerializeField] private int _blackWarriorsAmount;
    }
}
