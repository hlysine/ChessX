using ChessX.Game.Chess;
using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public partial class DrawableChessRuleset : DrawableRuleset<ChessPiece>
    {
        public DrawableChessRuleset(ChessMatch match)
            : base(match)
        {
        }

        protected override DrawableMatch<ChessPiece> CreateDrawableMatch(Match<ChessPiece> match) => new DrawableChessMatch((ChessMatch)match);
    }
}
