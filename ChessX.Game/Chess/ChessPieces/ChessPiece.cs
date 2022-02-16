using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public abstract class ChessPiece
    {
        public ChessColor Color { get; }
        public abstract ChessPieceType PieceType { get; }

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

        protected ChessPiece(ChessColor color)
        {
            Color = color;
        }

        public IEnumerable<Move> GetAllowedMoves(ChessMatch match)
        {
            // todo: filter by moves that would still leave the player in check
            return GetPossibleMoves(match);
        }

        protected abstract IEnumerable<Move> GetPossibleMoves(ChessMatch match);
    }
}
