using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputServiceMediator : IInputServiceMediator
    {
        public event Action<Vector2> OnClickedOnBoard;
        
        public void Notify(Vector2 pos)
        {
            OnClickedOnBoard?.Invoke(pos);
        }
    }
}