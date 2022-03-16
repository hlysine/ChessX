using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;
using ChessX.Game.Utils;

namespace ChessX.Game.Rulesets
{
    public abstract class Ruleset
    {
        public RulesetInfo RulesetInfo { get; }

        public abstract string Name { get; }

        public abstract IMatch CreateMatch();

        public abstract DrawableRuleset CreateDrawableRuleset(IMatch match);

        protected Ruleset()
        {
            RulesetInfo = new RulesetInfo(Name, GetType().GetInvariantInstantiationInfo());
        }
    }
}
