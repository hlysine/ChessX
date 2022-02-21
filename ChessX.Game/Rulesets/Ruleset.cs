using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets
{
    public abstract class Ruleset
    {
        public abstract string Name { get; }

        public abstract Match CreateMatch();

        public abstract DrawableRuleset CreateDrawableRuleset(Match match);
    }
}
