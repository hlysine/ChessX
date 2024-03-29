using System.Linq;
using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using static ChessX.Game.Rulesets.Chess.UI.CheckerboardTile;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public partial class Checkerboard : CompositeDrawable
    {
        public int BoardWidth { get; private set; }

        public int BoardHeight { get; private set; }

        [BackgroundDependencyLoader(true)]
        private void load(IHasBoardSize board)
        {
            BoardWidth = board?.BoardWidth ?? IMatch.DEFAULT_BOARD_SIZE.X;
            BoardHeight = board?.BoardHeight ?? IMatch.DEFAULT_BOARD_SIZE.Y;

            var tiles = new Drawable[BoardHeight][];

            for (int i = 0; i < BoardHeight; i++)
            {
                tiles[i] = new Drawable[BoardWidth];

                for (int j = 0; j < BoardWidth; j++)
                {
                    tiles[i][j] = new CheckerboardTile(i % 2 != j % 2 ? TileVariant.Dark : TileVariant.Light, new Vector2I(j, i));
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
