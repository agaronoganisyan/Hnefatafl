using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputHandler : IService
    {
        void ProcessClickOnBoard(Vector2 mousePosition);
    }
}