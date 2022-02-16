using ChessX.Game.Chess.Drawables;
using ChessX.Game.Chess.Rulesets;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Testing.Drawables.Steps;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneDrawableChessMatch : ChessXTestScene
    {
        [BackgroundDependencyLoader]
        private void load(Bindable<Ruleset> ruleset)
        {
            var classicRuleset = ruleset.Value;
            DrawableChessMatch match;
            var chessMatch = classicRuleset.CreateChessMatch();
            chessMatch.Initialize();
            Add(match = classicRuleset.CreateDrawableChessMatch(chessMatch));
            Add(new StepSlider<float>("Chess board rotation", 0, 360, 0) { ValueChanged = val => match.Rotation = val });
        }
    }
}
