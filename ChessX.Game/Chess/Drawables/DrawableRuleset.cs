using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace ChessX.Game.Chess.Drawables
{
    public abstract class DrawableRuleset : CompositeDrawable
    {
        public ChessMatch ChessMatch { get; }

        public DrawableChessMatch DrawableChessMatch { get; private set; }

        public abstract DrawableChessMatch CreateDrawableChessMatch();

        protected DrawableRuleset(ChessMatch match)
        {
            ChessMatch = match;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(DrawableChessMatch = CreateDrawableChessMatch());
        }
    }
}
