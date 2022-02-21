using ChessX.Game.Chess;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess
{
    public class DrawableChessMatch : DrawableMatch
    {
        public DrawableChessMatch(Match match)
            : base(match)
        {
        }

        protected override UI.DrawablePiece CreateDrawableRepresentation(Piece piece) => new DrawableChessPiece(piece);
    }
}
