namespace CodeBase.Infrastructure.Services.RuleManagerLogic
{
    public class SingleplayerRuleManager : RuleManager
    {
        protected override bool IsCanStartGame()
        {
            return true;
        }
    }
}