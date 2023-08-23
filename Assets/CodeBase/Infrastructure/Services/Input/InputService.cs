using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        private readonly GameInput _gameInput;
        public event Action<Vector2> OnClickedOnBoard;

        public InputService(IRuleManager ruleManager)
        {
            _gameInput = new GameInput();
                
            _gameInput.Gameplay.SetCallbacks(this);

            ruleManager.OnGameStarted += SetGameplayMode;
            ruleManager.OnGameFinished += SetUIMode;
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
                OnClickedOnBoard?.Invoke(Mouse.current.position.ReadValue());   
            }
        }
    }
}