using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        void SetGameplayMode();
        void SetUIMode();
        Action<Vector2> OnClickedOnBoard { get;  set; }
    }
}