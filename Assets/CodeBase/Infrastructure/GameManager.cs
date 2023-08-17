using System;

namespace CodeBase.Infrastructure
{
    public class GameManager : IGameManager
    {
        public event Action OnGameStarted;
        public event Action OnGameRestarted;
        public event Action OnGameFinished;
        public event Action OnWhiteTeamWon;
        public event Action OnBlackTeamWon;

        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;

        public void StartGame()
        {
            _isGameFinished = false;

            OnGameStarted?.Invoke();
        }

        public void RestartGame()
        {
            OnGameRestarted?.Invoke();

            StartGame();
        }

        public void BlackTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;
            
            OnBlackTeamWon?.Invoke();
            OnGameFinished?.Invoke();

        }

        public void WhiteTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;

            OnWhiteTeamWon?.Invoke();
            OnGameFinished?.Invoke();

        }
    }
}