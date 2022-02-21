using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI
{
    [Cached]
    public class PieceContainer : Container
    {
        public PieceContainer()
        {
            RelativeSizeAxes = Axes.Both;
            Padding = new MarginPadding(0.5f);
        }

        public delegate void PieceClickedHandler(DrawablePiece sender, ClickEvent e);

        public event PieceClickedHandler PieceClicked;

        public void TriggerPieceClick(DrawablePiece sender, ClickEvent e)
        {
            PieceClicked?.Invoke(sender, e);
        }
    }
}
