using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace ChessX.Game.Chess.Drawables
{
    public class CheckerboardTile : Box
    {
        [Resolved]
        private GridInputRedirector inputRedirector { get; set; }

        public TileVariant Variant { get; }

        public Vector2I GridPosition { get; }

        public CheckerboardTile(TileVariant variant, Vector2I position)
        {
            Variant = variant;
            GridPosition = position;
            Colour = getVariantColour();
            RelativeSizeAxes = Axes.Both;
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.FadeColour(Colour4.Gray, 100, Easing.InOutQuint);
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.FadeColour(getVariantColour(), 100, Easing.InOutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            return inputRedirector.SendEvent(GridPosition, e);
        }

        private ColourInfo getVariantColour()
        {
            return Variant == TileVariant.Dark ? Colour4.DarkGray : Colour4.LightGray;
        }

        public enum TileVariant
        {
            Light,
            Dark
        }
    }
}
