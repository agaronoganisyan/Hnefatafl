using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action<Vector2> OnClickedOnBoard;
    }
}