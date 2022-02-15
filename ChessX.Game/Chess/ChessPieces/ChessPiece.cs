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

        public ChessPiece(ChessColor color)
        {
            Color = color;
        }
    }
}
