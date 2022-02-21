using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;
using ChessX.Game.Utils;

namespace ChessX.Game.Rulesets
{
    public abstract class Ruleset
    {
        public RulesetInfo RulesetInfo { get; }

        public abstract string Name { get; }

        public abstract Match CreateMatch();

        public abstract DrawableRuleset CreateDrawableRuleset(Match match);

        protected Ruleset()
        {
            RulesetInfo = new RulesetInfo(Name, GetType().GetInvariantInstantiationInfo());
        }
    }
}
