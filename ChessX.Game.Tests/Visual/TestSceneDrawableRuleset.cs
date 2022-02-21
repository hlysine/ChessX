using System.Threading;
using System.Threading.Tasks;
using ChessX.Game.Rulesets;
using ChessX.Game.Rulesets.Chess;
using ChessX.Game.Rulesets.Chess.UI;
using ChessX.Game.Rulesets.Chess.UI.MoveSelection;
using ChessX.Game.Utils;
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
            var chessRuleset = (ChessRuleset)ruleset.Value;
            ChessMoveSelector control1 = new ChessMoveSelector();
            ChessMoveSelector control2 = new ChessMoveSelector();
            DrawableChessRuleset drawableRuleset;
            var chessMatch = (ChessMatch)chessRuleset.CreateMatch();
            chessMatch.AddPlayer(control1.Player.With(p => p.Color = ChessColor.White));
            chessMatch.AddPlayer(control2.Player.With(p => p.Color = ChessColor.Black));
            chessMatch.Initialize();
            Add(drawableRuleset = (DrawableChessRuleset)chessRuleset.CreateDrawableRuleset(chessMatch));
            drawableRuleset.DrawableMatch.Overlays.Add(control1);
            drawableRuleset.DrawableMatch.Overlays.Add(control2);

            Add(new StepSlider<float>("Chess board rotation", 0, 360, 0) { ValueChanged = val => drawableRuleset.DrawableMatch.Rotation = val });

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
