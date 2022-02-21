using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess
{
    public class DrawableChessRuleset : DrawableRuleset
    {
        public DrawableChessRuleset(Match match)
            : base(match)
        {
        }

        public override DrawableMatch CreateDrawableMatch() => new DrawableChessMatch(Match);
    }
}
