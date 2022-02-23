using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace ChessX.Game.Rulesets.UI
{
    /// <summary>
    /// A container that provides a grid coordinate system for children.
    /// </summary>
    public class BoardScalingContainer : Container
    {
        protected override Container<Drawable> Content => content;
        private readonly ScalingContainer content;

        public int BoardWidth
        {
            get => content.TargetWidth;
            set => content.TargetWidth = value;
        }

        public int BoardHeight
        {
            get => content.TargetHeight;
            set => content.TargetHeight = value;
        }

        public BoardScalingContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;

            InternalChild = content = new ScalingContainer { RelativeSizeAxes = Axes.Both };
        }

        [BackgroundDependencyLoader(true)]
        private void load(IHasBoardSize board)
        {
            BoardWidth = board?.BoardWidth ?? Match.DEFAULT_BOARD_SIZE.X;
            BoardHeight = board?.BoardHeight ?? Match.DEFAULT_BOARD_SIZE.Y;
            FillMode = FillMode.Fit;
            FillAspectRatio = (float)BoardWidth / BoardHeight;
        }
    }
}
