using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets
{
    public abstract class Ruleset
    {
        public abstract string Name { get; }

        public abstract ChessMatch CreateChessMatch();

        public abstract DrawableRuleset CreateDrawableRuleset(ChessMatch match);
    }
}
