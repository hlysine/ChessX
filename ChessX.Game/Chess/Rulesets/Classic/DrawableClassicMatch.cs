using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets.Classic
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
