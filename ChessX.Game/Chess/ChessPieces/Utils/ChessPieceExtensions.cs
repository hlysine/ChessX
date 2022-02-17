using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces.Utils
{
    public static class ChessPieceExtensions
    {
        public static ChessPiece WithPosition(this ChessPiece piece, int x, int y)
        {
            piece.X = x;
            piece.Y = y;
            return piece;
        }

        public static ChessPiece WithPosition(this ChessPiece piece, Vector2I position)
        {
            piece.Position = position;
            return piece;
        }
    }
}
