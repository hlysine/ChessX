using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace ChessX.Game.Chess.Drawables
{
    public class CheckerboardTile : Box
    {
        public TileVariant Variant { get; }

        public CheckerboardTile(TileVariant variant)
        {
            Variant = variant;
            Colour = getVariantColour();
            RelativeSizeAxes = Axes.Both;
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.FadeColour(Colour4.Gray, 100);
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.FadeColour(getVariantColour(), 100);
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
