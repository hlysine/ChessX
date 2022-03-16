using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess
{
    public abstract class Piece
    {
        public readonly Bindable<Vector2I> PositionBindable = new Bindable<Vector2I>();

        public Vector2I Position
        {
            get => PositionBindable.Value;
            set => PositionBindable.Value = value;
        }

        public int X
        {
            get => Position.X;
            set => Position = new Vector2I(value, Position.Y);
        }

        public int Y
        {
            get => Position.Y;
            set => Position = new Vector2I(Position.X, value);
        }

        public abstract IEnumerable<Move> GetAllowedMoves(IMatch match, bool noRecursion = false);
    }

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

        public static TPiece WithPosition<TPiece>(this TPiece piece, int x, int y) where TPiece : Piece
        {
            piece.X = x;
            piece.Y = y;
            return piece;
        }

        public static TPiece WithPosition<TPiece>(this TPiece piece, Vector2I position) where TPiece : Piece
        {
            piece.Position = position;
            return piece;
        }
    }
}
