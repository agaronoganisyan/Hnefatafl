using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputServiceMediator 
    {
        event Action<Vector2> OnClickedOnBoard;
        void Notify(Vector2 pos);
    }
}