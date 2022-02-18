using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class DrawableClassicRuleset : DrawableRuleset
    {
        public DrawableClassicRuleset(ChessMatch match)
            : base(match)
        {
        }

        public override DrawableChessMatch CreateDrawableChessMatch() => new DrawableClassicMatch(ChessMatch);
    }
}
