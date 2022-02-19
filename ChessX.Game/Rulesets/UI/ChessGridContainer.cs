using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Rulesets.UI
{
    /// <summary>
    /// A container that aligns its children to the chess grid.
    /// </summary>
    public class ChessGridContainer : Container
    {
        protected override Container<Drawable> Content => content;
        private readonly ScalingContainer content;

        public int BoardWidth
        {
            set => content.TargetWidth = value;
        }

        public int BoardHeight
        {
            set => content.TargetHeight = value;
        }

        public ChessGridContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;

            InternalChild = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                FillAspectRatio = 1,
                Child = content = new ScalingContainer { RelativeSizeAxes = Axes.Both }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(IHasBoardSize board)
        {
            BoardWidth = board?.BoardWidth ?? ChessMatch.DEFAULT_BOARD_SIZE.X;
            BoardHeight = board?.BoardHeight ?? ChessMatch.DEFAULT_BOARD_SIZE.Y;
        }

        /// <summary>
        /// A <see cref="Container"/> which scales its content relative to a target width and height.
        /// </summary>
        private class ScalingContainer : Container
        {
            internal int TargetWidth { get; set; }

            internal int TargetHeight { get; set; }

            protected override void Update()
            {
                base.Update();

                Scale = new Vector2(Parent.ChildSize.X / TargetWidth, Parent.ChildSize.Y / TargetHeight);
                Size = Vector2.Divide(Vector2.One, Scale);
            }
        }
    }
}
