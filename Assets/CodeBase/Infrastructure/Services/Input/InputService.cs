using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService, IGameplayModeChangingObserver
    {
        public IInputServiceMediator InputServiceMediator => _serviceMediator;
        private IInputServiceMediator _serviceMediator;
        
        private GameInput _gameInput;
        private IInputHandler _inputHandler;
        private IRuleManager _ruleManager;
        private IGameRoomHandler _gameRoomHandler;
        
        public void Initialize()
        {
            _serviceMediator = new InputServiceMediator();
            _gameInput = new GameInput();

            _gameInput.Gameplay.ClickOnBoard.canceled += OnClickOnBoard;
            
            SetUIMode();
            
            _inputHandler = ServiceLocator.Get<IInputHandler>();
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
            
            _inputHandler = ServiceLocator.Get<IInputHandler>();
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

        void OnClickOnBoard(InputAction.CallbackContext context)
        {
            _inputHandler.ProcessClickOnBoard(_gameInput.Gameplay.ClickPosition.ReadValue<Vector2>());
        }
    }
}