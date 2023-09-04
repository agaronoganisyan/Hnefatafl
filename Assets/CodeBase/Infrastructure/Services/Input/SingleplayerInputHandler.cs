namespace CodeBase.Infrastructure.Services.Input
{
    public class SingleplayerInputHandler : InputHandler
    {
        protected override bool IsCanProcessClick()
        {
            return true;
        }
    }
}