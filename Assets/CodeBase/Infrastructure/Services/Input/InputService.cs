using System;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService, GameInput.IGameplayActions, IGameplayModeChangingObserver
    {
        public IInputServiceMediator InputServiceMediator => _serviceMediator;
        private IInputServiceMediator _serviceMediator;
        
        private GameInput _gameInput;
        private IRuleManager _ruleManager;
        private IGameRoomHandler _gameRoomHandler;
        
        public void Initialize()
        {
            _serviceMediator = new InputServiceMediator();
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);

            SetUIMode();

            _ruleManager = ServiceLocator.Get<IRuleManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            
            _ruleManager.RuleManagerMediator.OnGameStarted += SetGameplayMode;
            _ruleManager.RuleManagerMediator.OnGameFinished += SetUIMode;
            _gameRoomHandler.Mediator.OnQuitRoom += SetUIMode;
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }
        
        public void UpdateChangedProperties()
        {
            _ruleManager.RuleManagerMediator.OnGameStarted -= SetGameplayMode;
            _ruleManager.RuleManagerMediator.OnGameFinished -= SetUIMode;
            _gameRoomHandler.Mediator.OnQuitRoom -= SetUIMode;
            
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            
            _ruleManager.RuleManagerMediator.OnGameStarted += SetGameplayMode;
            _ruleManager.RuleManagerMediator.OnGameFinished += SetUIMode;
            _gameRoomHandler.Mediator.OnQuitRoom += SetUIMode;
        }
        
        void SetGameplayMode()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }
        
        void SetUIMode()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
        }

        public void OnClickOnBoard(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
#if UNITY_EDITOR
                _serviceMediator.Notify(Mouse.current.position.ReadValue());
#elif UNITY_ANDROID || UNITY_IOS
                _serviceMediator.Notify(Touchscreen.current.primaryTouch.position.ReadValue());
#else
                _serviceMediator.Notify(Mouse.current.position.ReadValue());
#endif

            }
        }
    }
}