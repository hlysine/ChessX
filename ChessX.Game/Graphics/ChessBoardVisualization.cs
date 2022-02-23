using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;

namespace ChessX.Game.Graphics
{
    [Cached(typeof(IHasBoardSize))]
    public class ChessBoardVisualization : CompositeDrawable, IHasBoardSize
    {
        public Vector2I BoardSize { get; }

        public ChessBoardVisualization(Vector2I boardSize)
        {
            BoardSize = boardSize;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new ScalingContainer
            {
                TargetWidth = BoardSize.X,
                TargetHeight = BoardSize.Y,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
                FillAspectRatio = (float)BoardSize.X / BoardSize.Y,
                Children = new[]
                {
                    new AnimatingCheckerboard
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            }.WithEffect(new BlurEffect(), c => c.RelativeSizeAxes = Axes.Both);
        }

        private class AnimatingCheckerboard : CompositeDrawable
        {
            public int BoardWidth { get; private set; }

            public int BoardHeight { get; private set; }

            [BackgroundDependencyLoader(true)]
            private void load(IHasBoardSize board, ChessXColor chessXColor)
            {
                BoardWidth = board?.BoardWidth ?? Match.DEFAULT_BOARD_SIZE.X;
                BoardHeight = board?.BoardHeight ?? Match.DEFAULT_BOARD_SIZE.Y;

                var tiles = new Drawable[BoardHeight][];

                for (int i = 0; i < BoardHeight; i++)
                {
                    tiles[i] = new Drawable[BoardWidth];

                    for (int j = 0; j < BoardWidth; j++)
                    {
                        if (i % 2 != j % 2)
                        {
                            tiles[i][j] = new AnimatingCheckerboardTile
                            {
                                Colour = Colour4.Gray,
                                Color1 = chessXColor.ChessBoardDark,
                                Color2 = chessXColor.ChessBoardDark.Darken(0.03f)
                            };
                        }
                        else
                        {
                            tiles[i][j] = new AnimatingCheckerboardTile
                            {
                                Colour = Colour4.Gray,
                                Color1 = chessXColor.ChessBoardLight,
                                Color2 = chessXColor.ChessBoardLight.Lighten(0.03f)
                            };
                        }
                    }
                }

                InternalChild = new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = Enumerable.Range(0, BoardWidth).Select(_ => new Dimension()).ToArray(),
                    RowDimensions = Enumerable.Range(0, BoardHeight).Select(_ => new Dimension()).ToArray(),
                    Content = tiles
                };
            }
        }

        private class AnimatingCheckerboardTile : Box
        {
            private Colour4 color1;

            public Colour4 Color1
            {
                get => color1;
                set
                {
                    color1 = value;
                    if (IsLoaded)
                        updateTransforms();
                }
            }

            private Colour4 color2;

            public Colour4 Color2
            {
                get => color2;
                set
                {
                    color2 = value;
                    if (IsLoaded)
                        updateTransforms();
                }
            }

            public AnimatingCheckerboardTile()
            {
                RelativeSizeAxes = Axes.Both;
            }

            protected override void LoadComplete()
            {
                updateTransforms();
            }

            private void updateTransforms()
            {
                ClearTransforms();
                this.Loop(d => d.FadeColour(Color1, RNG.Next(2000) + 1000, Easing.InOutQuint).Then(c => c.FadeColour(Color2, RNG.Next(2000) + 1000, Easing.InOutQuint)));
            }
        }
    }
}
