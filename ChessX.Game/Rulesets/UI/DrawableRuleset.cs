using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class DrawableRuleset : CompositeDrawable
    {
    }

    public abstract class DrawableRuleset<TPiece> : DrawableRuleset where TPiece : Piece
    {
        public Match<TPiece> Match { get; }

        public DrawableMatch<TPiece> DrawableMatch { get; private set; }

        public abstract DrawableMatch<TPiece> CreateDrawableMatch();

        protected DrawableRuleset(Match<TPiece> match)
        {
            Match = match;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(DrawableMatch = CreateDrawableMatch());
        }
    }
}
