using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        private readonly GameInput _gameInput;
        private readonly IInputServiceMediator _serviceMediator;
        
        public InputService(IInputServiceMediator inputServiceMediator, IRuleManagerMediator ruleManagerMediator)
        {
            _serviceMediator = inputServiceMediator;
            
            _gameInput = new GameInput();
                
            _gameInput.Gameplay.SetCallbacks(this);
            
            ruleManagerMediator.OnGameStarted += SetGameplayMode;
            ruleManagerMediator.OnGameFinished += SetUIMode;
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