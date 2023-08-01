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
    public enum GameState
    {
        None,
        Active,
        Won,
        Defeat
    }

    public class GameManager : IService
    {
        public static event Action OnGameWon;
        public static event Action OnGameDefeated;

        public void InitializeGame()
        {
            ServiceLocator.Get<Board>().Initialize();
            ServiceLocator.Get<BoardHighlight>().Initialize();
            ServiceLocator.Get<UnitsManager>().Initialize(this,ServiceLocator.Get<Board>());
            ServiceLocator.Get<Controller>().Initialize(ServiceLocator.Get<UnitsManager>());

            ServiceLocator.Get<GameplayCanvas>().Initialize();
            ServiceLocator.Get<DebriefingCanvas>().Initialize(this);
        }

        public void StartGame()
        {
            ServiceLocator.Get<DebriefingCanvas>().Close();
            ServiceLocator.Get<GameplayCanvas>().Open();
            ServiceLocator.Get<Controller>().Prepare();
        }

        public void RestartGame()
        {
            ServiceLocator.Get<UnitsManager>().Restart();
            ServiceLocator.Get<BoardHighlight>().Restart();

            StartGame();
        }

        public void DefeatGame()
        {
            OnGameDefeated?.Invoke();
        }

        public void WinGame()
        {
            OnGameWon?.Invoke();
        }
    }
}