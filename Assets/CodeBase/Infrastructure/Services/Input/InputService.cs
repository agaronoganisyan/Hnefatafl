using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService :  IInputService, GameInput.IGameplayActions
    {
        private GameInput _gameInput;
        public Action OnClicked { get; }
        public InputService()
        {
            _gameInput = new GameInput();
                
            _gameInput.Gameplay.SetCallbacks(this);
            //_gameInput.UI.SetCallbacks(this);
        }

        public void SetGameplay()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }

        public void SetUI()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
        }
        
        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Debug.Log("OnClick");
                OnClicked?.Invoke();   
            }
        }
    }
}