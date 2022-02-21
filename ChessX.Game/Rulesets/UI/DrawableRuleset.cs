using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class DrawableRuleset : CompositeDrawable
    {
        public Match Match { get; }

        public DrawableMatch DrawableMatch { get; private set; }

        public abstract DrawableMatch CreateDrawableMatch();

        protected DrawableRuleset(Match match)
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
