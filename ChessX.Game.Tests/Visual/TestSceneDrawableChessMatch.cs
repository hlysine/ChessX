using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Testing.Drawables.Steps;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneDrawableChessMatch : ChessXTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            DrawableChessMatch match;
            var chessMatch = new ClassicMatch();
            chessMatch.Initialize();
            Add(match = new DrawableChessMatch(chessMatch));
            Add(new StepSlider<float>("Chess board rotation", 0, 360, 0) { ValueChanged = val => match.Rotation = val });
        }
    }
}
