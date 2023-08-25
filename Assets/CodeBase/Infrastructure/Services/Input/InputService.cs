using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        private  GameInput _gameInput;
        private  IInputServiceMediator _serviceMediator;

        public void Initialize()
        {
            _serviceMediator = ServiceLocator.Get<IInputServiceMediator>();
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            
            ServiceLocator.Get<IRuleManagerMediator>().OnGameStarted += SetGameplayMode;
            ServiceLocator.Get<IRuleManagerMediator>().OnGameFinished += SetUIMode;
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