using ChessX.Game.Rulesets.Chess.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace ChessX.Game.Tests.Visual
{
    public partial class TestSceneCheckerboard : ChessXTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new Checkerboard
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                FillAspectRatio = 1
            });
        }
    }
}
