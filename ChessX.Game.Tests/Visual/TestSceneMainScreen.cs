using ChessX.Game.Screens;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace ChessX.Game.Tests.Visual
{
    public partial class TestSceneMainScreen : ChessXTestScene
    {
        public TestSceneMainScreen()
        {
            Add(new ScreenStack(new MenuScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
