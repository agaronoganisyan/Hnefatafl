using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputHandler
    {
        void ProcessClickOnBoard(Vector2 mousePosition);
    }
}