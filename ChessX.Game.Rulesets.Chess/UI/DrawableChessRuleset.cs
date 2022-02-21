using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public class DrawableChessRuleset : DrawableRuleset<ChessPiece>
    {
        public DrawableChessRuleset(ChessMatch match)
            : base(match)
        {
        }

        public override DrawableMatch<ChessPiece> CreateDrawableMatch() => new DrawableChessMatch((ChessMatch)Match);
    }
}
