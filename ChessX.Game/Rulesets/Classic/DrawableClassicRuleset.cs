using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Classic
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
