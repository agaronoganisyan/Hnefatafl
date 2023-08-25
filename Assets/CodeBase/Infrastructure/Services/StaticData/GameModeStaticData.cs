using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public enum GameModeType
    {
        None,
        Classic
    }

    [CreateAssetMenu(fileName = "ModeStaticData", menuName = "StaticData/GameMode")]
    public class GameModeStaticData : ScriptableObject
    {
        public GameModeType GameModeType => _gameModeType;
        [SerializeField] private GameModeType _gameModeType;
        public int BoardSize => _boardSize;
        [SerializeField] private int _boardSize;
        
        public int WhiteWarriorsAmount => _whiteWarriorsAmount;
        [SerializeField] private int _whiteWarriorsAmount;
        
        public int BlackWarriorsAmount => _blackWarriorsAmount;
        [SerializeField] private int _blackWarriorsAmount;
    }
}
