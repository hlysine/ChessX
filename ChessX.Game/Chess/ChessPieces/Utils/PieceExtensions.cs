using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces.Utils
{
    public static class PieceExtensions
    {
        public static Piece WithPosition(this Piece piece, int x, int y)
        {
            piece.X = x;
            piece.Y = y;
            return piece;
        }

        public static Piece WithPosition(this Piece piece, Vector2I position)
        {
            piece.Position = position;
            return piece;
        }
    }
}
