using ChessX.Game.Chess.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Testing.Drawables.Steps;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneCheckerboard : ChessXTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        [BackgroundDependencyLoader]
        private void load()
        {
            DrawableChessMatch match;
            Add(match = new DrawableChessMatch());
            Add(new StepSlider<float>("Chess board rotation", 0, 360, 0) { ValueChanged = val => match.Rotation = val });
        }
    }
}
