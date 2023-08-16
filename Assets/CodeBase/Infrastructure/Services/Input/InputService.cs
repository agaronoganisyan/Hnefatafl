using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        private GameInput _gameInput;
        public Action<Vector2> OnClickedOnBoard { get; set; }

        public InputService()
        {
            _gameInput = new GameInput();
                
            _gameInput.Gameplay.SetCallbacks(this);
        }

        public void SetGameplayMode()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }

        public void SetUIMode()
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