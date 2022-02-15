using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    /// <summary>
    /// A container that aligns its children to the chess grid.
    /// </summary>
    public class ChessGridContainer : Container
    {
        protected override Container<Drawable> Content => content;
        private readonly ScalingContainer content;

        public int BoardWidth { get; }

        public int BoardHeight { get; }

        public bool AlignToTileCenter
        {
            set => content.AlignToCenter = value;
        }

        public ChessGridContainer(int width = 8, int height = 8)
        {
            BoardWidth = width;
            BoardHeight = height;

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
                Child = content = new ScalingContainer(BoardWidth, BoardHeight) { RelativeSizeAxes = Axes.Both }
            };
        }

        /// <summary>
        /// A <see cref="Container"/> which scales its content relative to a target width and height.
        /// </summary>
        private class ScalingContainer : Container
        {
            internal int TargetWidth { get; }

            internal int TargetHeight { get; }

            internal bool AlignToCenter { get; set; }

            internal ScalingContainer(int width, int height)
            {
                TargetWidth = width;
                TargetHeight = height;
            }

            protected override void Update()
            {
                base.Update();

                Scale = new Vector2(Parent.ChildSize.X / TargetWidth, Parent.ChildSize.Y / TargetHeight);
                Padding = AlignToCenter ? new MarginPadding { Horizontal = 0.5f, Vertical = 0.5f } : new MarginPadding();
                Size = Vector2.Divide(Vector2.One, Scale);
            }
        }
    }
}
