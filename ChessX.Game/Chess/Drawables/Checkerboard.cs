using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using static ChessX.Game.Chess.Drawables.CheckerboardTile;

namespace ChessX.Game.Chess.Drawables
{
    public class Checkerboard : CompositeDrawable
    {
        public int BoardWidth { get; }

        public int BoardHeight { get; }

        public Checkerboard(int width = 8, int height = 8)
        {
            BoardWidth = width;
            BoardHeight = height;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var tiles = new Drawable[BoardHeight][];

            for (int i = 0; i < BoardHeight; i++)
            {
                tiles[i] = new Drawable[BoardWidth];

                for (int j = 0; j < BoardWidth; j++)
                {
                    tiles[i][j] = new CheckerboardTile(i % 2 == j % 2 ? TileVariant.Dark : TileVariant.Light);
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
}
