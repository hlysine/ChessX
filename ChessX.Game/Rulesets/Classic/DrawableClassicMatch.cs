using ChessX.Game.Chess;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Classic
{
    public class DrawableClassicMatch : DrawableChessMatch
    {
        public DrawableClassicMatch(ChessMatch match)
            : base(match)
        {
        }

        protected override DrawableChessPiece CreateDrawableRepresentation(ChessPiece chessPiece) => new DrawableClassicPiece(chessPiece);
    }
}
