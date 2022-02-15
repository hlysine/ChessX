using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess
{
    public interface IHasBoardSize
    {
        public Vector2I BoardSize { get; }

        public int BoardWidth => BoardSize.X;

        public int BoardHeight => BoardSize.Y;
    }
}
