using ChessX.Game.Graphics;
using ChessX.Game.Rulesets.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public partial class CheckerboardTile : Box
    {
        [Resolved(canBeNull: true)]
        private GridInputRedirector inputRedirector { get; set; }

        [Resolved]
        private ChessXColor chessXColor { get; set; }

        public TileVariant Variant { get; }

        public Vector2I GridPosition { get; }

        public CheckerboardTile(TileVariant variant, Vector2I position)
        {
            Variant = variant;
            GridPosition = position;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Colour = getVariantColour();
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.FadeColour(getVariantColour().Darken(0.3f), 100, Easing.InOutQuint);
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.FadeColour(getVariantColour(), 100, Easing.InOutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            return inputRedirector?.SendEvent(GridPosition, e) ?? base.OnClick(e);
        }

        private Colour4 getVariantColour()
        {
            return Variant == TileVariant.Dark ? chessXColor.ChessBoardDark : chessXColor.ChessBoardLight;
        }

        public enum TileVariant
        {
            Light,
            Dark
        }
    }
}
