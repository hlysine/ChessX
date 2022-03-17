using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class DrawableRuleset : CompositeDrawable
    {
        public IMatch Match { get; }

        protected DrawableRuleset(IMatch match)
        {
            Match = match;
            RelativeSizeAxes = Axes.Both;
        }
    }

    public abstract class DrawableRuleset<TPiece> : DrawableRuleset where TPiece : Piece
    {
        public new Match<TPiece> Match { get; }

        public DrawableMatch<TPiece> DrawableMatch { get; private set; }

        protected DrawableRuleset(Match<TPiece> match)
            : base(match)
        {
            Match = match;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(DrawableMatch = CreateDrawableMatch(Match));
        }

        protected abstract DrawableMatch<TPiece> CreateDrawableMatch(Match<TPiece> match);
    }
}
