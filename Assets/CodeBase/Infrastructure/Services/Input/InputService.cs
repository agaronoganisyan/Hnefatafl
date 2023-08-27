using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        public IInputServiceMediator InputServiceMediator => _serviceMediator;
        private IInputServiceMediator _serviceMediator;
        
        private  GameInput _gameInput;

        public void Initialize()
        {
            _serviceMediator = new InputServiceMediator();
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            
            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameStarted += SetGameplayMode;
            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameFinished += SetUIMode;
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
#endif
            }
        }
    }
}