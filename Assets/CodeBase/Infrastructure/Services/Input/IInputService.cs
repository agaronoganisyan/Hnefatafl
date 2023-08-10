using System;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Action OnClicked { get; }  
    }
}