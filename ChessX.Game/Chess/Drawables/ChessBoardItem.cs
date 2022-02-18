using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public abstract class ChessBoardItem : CompositeDrawable, IReceiveGridInput
    {
        protected ChessBoardItem()
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
            Size = Vector2.One;
        }

        public abstract Vector2I GridPosition { get; }
    }
}
