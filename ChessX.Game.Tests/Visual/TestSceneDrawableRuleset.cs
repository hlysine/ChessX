using System.Threading;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Players;
using ChessX.Game.Rulesets;
using ChessX.Game.Rulesets.UI;
using ChessX.Game.Rulesets.UI.MoveSelection;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Testing.Drawables.Steps;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneDrawableRuleset : ChessXTestScene
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        [BackgroundDependencyLoader]
        private void load(Bindable<Ruleset> ruleset)
        {
            var classicRuleset = ruleset.Value;
            MoveSelector control1 = new MoveSelector();
            MoveSelector control2 = new MoveSelector();
            Player player1 = control1.Player;
            Player player2 = control2.Player;
            DrawableRuleset drawableRuleset;
            var chessMatch = classicRuleset.CreateChessMatch();
            chessMatch.AddPlayer(player1, ChessColor.White);
            chessMatch.AddPlayer(player2, ChessColor.Black);
            chessMatch.Initialize();
            Add(drawableRuleset = classicRuleset.CreateDrawableRuleset(chessMatch));
            drawableRuleset.DrawableChessMatch.Overlays.Add(control1);
            drawableRuleset.DrawableChessMatch.Overlays.Add(control2);

            Add(new StepSlider<float>("Chess board rotation", 0, 360, 0) { ValueChanged = val => drawableRuleset.DrawableChessMatch.Rotation = val });

            async void loop(CancellationToken token = default)
            {
                while (!chessMatch.MatchEnded && !token.IsCancellationRequested)
                    await chessMatch.ProcessRound();
            }

            Task.Factory.StartNew(() => loop(cancellationTokenSource.Token), TaskCreationOptions.LongRunning);
        }

        protected override void Dispose(bool isDisposing)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
