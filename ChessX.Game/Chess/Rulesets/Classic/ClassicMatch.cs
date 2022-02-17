using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class ClassicMatch : ChessMatch
    {
        public override Vector2I BoardSize => DEFAULT_BOARD_SIZE;

        public ClassicMatch(Ruleset ruleset)
            : base(ruleset)
        {
        }

        public override async Task ProcessRound()
        {
            var whiteMove = await WhitePlayer.PerformMoveAsync();
            executeMove(whiteMove);
            MoveHistory.Add(whiteMove);
            await Task.Delay(1000);
            var blackMove = await BlackPlayer.PerformMoveAsync();
            executeMove(blackMove);
            MoveHistory.Add(blackMove);
            await Task.Delay(1000);
        }

        private void executeMove(Move move)
        {
            var instructions = move.GetInstructions(this).ToList();

            foreach (var instruction in instructions)
            {
                instruction.Execute(this);
            }
        }

        public override bool MatchEnded => false;
    }
}
