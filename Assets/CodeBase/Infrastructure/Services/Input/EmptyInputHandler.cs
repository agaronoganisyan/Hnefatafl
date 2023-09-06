namespace CodeBase.Infrastructure.Services.Input
{
    public class EmptyInputHandler : InputHandler
    {
        protected override bool IsCanProcessClick()
        {
            return false;
        }
    }
}