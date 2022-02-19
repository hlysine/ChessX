using ChessX.Game.Graphics;
using osu.Framework.Graphics;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneSpinningBox : ChessXTestScene
    {
        public TestSceneSpinningBox()
        {
            Add(new SpinningBox
            {
                Anchor = Anchor.Centre,
            });
        }
    }
}
