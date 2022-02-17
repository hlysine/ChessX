using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace ChessX.Game.Chess.Players
{
    public abstract class Player
    {
        public ChessMatch ChessMatch { get; }

        public readonly BindableBool IsInTurnBindable = new BindableBool();

        public bool IsInTurn
        {
            get => IsInTurnBindable.Value;
            set => IsInTurnBindable.Value = value;
        }

        public readonly Bindable<Move> SelectedMoveBindable = new Bindable<Move>();

        [CanBeNull]
        public Move SelectedMove
        {
            get => SelectedMoveBindable.Value;
            set => SelectedMoveBindable.Value = value;
        }

        protected Player(ChessMatch chessMatch)
        {
            ChessMatch = chessMatch;
        }

        public async Task<Move> PerformMoveAsync()
        {
            SelectedMove = null;
            IsInTurn = true;

            var move = await PerformMoveInternalAsync().ConfigureAwait(false);

            SelectedMove = move;
            IsInTurn = false;

            return move;
        }

        protected abstract Task<Move> PerformMoveInternalAsync();
    }
}
