using System;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace ChessX.Game.Chess.Players
{
    public abstract class Player
    {
        public ChessMatch ChessMatch { get; set; }

        public ChessColor Color { get; set; }

        public readonly BindableBool IsInTurnBindable = new BindableBool();

        public bool IsInTurn
        {
            get => IsInTurnBindable.Value;
            set => IsInTurnBindable.Value = value;
        }

        public readonly Bindable<Move> SelectedMoveBindable = new Bindable<Move>();
        private readonly LeasedBindable<Move> selectedMoveBindable;

        [CanBeNull]
        public Move SelectedMove => SelectedMoveBindable.Value;

        [CanBeNull]
        private Move selectedMove
        {
            get => selectedMoveBindable.Value;
            set => selectedMoveBindable.Value = value;
        }

        public abstract bool RotateChessBoard { get; }

        public delegate void TurnStartHandler(Action<Move> selectMove);

        public event TurnStartHandler TurnStarted;

        public event Action TurnEnded;

        private TaskCompletionSource<Move> turnCompletion;

        protected Player()
        {
            selectedMoveBindable = SelectedMoveBindable.BeginLease(false);
        }

        public void StartTurn()
        {
            if (IsInTurn) return;

            selectedMove = null;
            IsInTurn = true;
            var localCompletionSource = turnCompletion = new TaskCompletionSource<Move>();

            void selectMove(Move move)
            {
                if (localCompletionSource == turnCompletion)
                    selectedMove = move;
            }

            OnTurnStart(selectMove);
            TurnStarted?.Invoke(selectMove);
        }

        public void EndTurn()
        {
            if (!IsInTurn) return;

            IsInTurn = false;
            turnCompletion?.SetResult(SelectedMove);
            turnCompletion = null;
            OnTurnEnd();
            TurnEnded?.Invoke();
        }

        public Task WaitForTurnEnd()
        {
            return turnCompletion.Task;
        }

        protected virtual void OnTurnStart(Action<Move> selectMove) { }

        protected virtual void OnTurnEnd() { }
    }
}
