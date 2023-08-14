using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic;
using CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic;

namespace CodeBase.Infrastructure
{
    public class GameManager : IService
    {
        public static event Action OnGameStarted;
        public static event Action OnGameRestarted;
        public static event Action OnWhiteTeamWon;
        public static event Action OnBlackTeamWon;

        bool _isGameFinished;
        public bool IsGameFinished => _isGameFinished;

        public void InitializeGame()
        {
            //ServiceLocator.Get<Board>().Initialize();
            //ServiceLocator.Get<BoardHighlight>().Initialize();
            //ServiceLocator.Get<UnitsManager>().Initialize(this,ServiceLocator.Get<Board>());
            //ServiceLocator.Get<Controller>().Initialize(ServiceLocator.Get<UnitsManager>());

            ServiceLocator.Get<GameplayCanvas>().Initialize();
            ServiceLocator.Get<DebriefingCanvas>().Initialize(this);
        }

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
        }

        public void WhiteTeamWin()
        {
            if (_isGameFinished) return;
            _isGameFinished = true;

            OnWhiteTeamWon?.Invoke();
        }
    }
}