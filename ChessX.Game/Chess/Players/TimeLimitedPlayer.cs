using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.Players
{
    public abstract class TimeLimitedPlayer : Player
    {
        protected TimeLimitedPlayer(ChessMatch chessMatch)
            : base(chessMatch)
        {
        }

        private readonly Stopwatch stopwatch = new Stopwatch();

        public TimeSpan TimeAllowed { get; private set; }

        public TimeSpan TimeRemaining => TimeAllowed - stopwatch.Elapsed;

        protected override async Task<Move> PerformMoveInternalAsync()
        {
            TimeAllowed = GetTimeAllowed();
            var cancellationTokenSource = new CancellationTokenSource();

            var timeTask = Task.Delay(TimeAllowed, cancellationTokenSource.Token);
            var moveTask = PerformTimeLimitedMoveAsync(cancellationTokenSource.Token);

            stopwatch.Restart();
            var completedTask = await Task.WhenAny(timeTask, moveTask);
            stopwatch.Stop();
            cancellationTokenSource.Cancel();

            OnMoveComplete(stopwatch.Elapsed, completedTask == moveTask);

            return completedTask == moveTask ? moveTask.Result : null;
        }

        protected abstract TimeSpan GetTimeAllowed();

        protected virtual void OnMoveComplete(TimeSpan timeSpent, bool moveSubmitted)
        {
        }

        protected abstract Task<Move> PerformTimeLimitedMoveAsync(CancellationToken token = default);
    }
}
