using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets
{
    public abstract class Ruleset
    {
        public abstract string Name { get; }

        public abstract ChessMatch CreateChessMatch();

        public abstract DrawableRuleset CreateDrawableRuleset(ChessMatch match);
    }
}
