#nullable enable

using System;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using osu.Framework.Bindables;

namespace ChessX.Game.Players
{
    public abstract class Player<TPiece> : IPlayer<TPiece> where TPiece : Piece
    {
        public Match<TPiece>? Match { get; set; }

        public readonly BindableBool IsInTurnBindable = new();

        public bool IsInTurn
        {
            get => IsInTurnBindable.Value;
            set => IsInTurnBindable.Value = value;
        }

        public readonly Bindable<Move<TPiece>?> SelectedMoveBindable = new();
        private readonly LeasedBindable<Move<TPiece>?> selectedMoveBindable;

        public Move<TPiece>? SelectedMove => SelectedMoveBindable.Value;

        private Move<TPiece>? selectedMove
        {
            get => selectedMoveBindable.Value;
            set => selectedMoveBindable.Value = value;
        }

        public abstract bool RotateBoardInTurn { get; }

        public abstract float TargetBoardRotation { get; }

        public event IPlayer<TPiece>.TurnStartHandler? TurnStarted;

        public event Action? TurnEnded;

        private TaskCompletionSource<Move<TPiece>?>? turnCompletion;

        protected Player()
        {
            selectedMoveBindable = SelectedMoveBindable.BeginLease(false);
        }

        public void StartTurn()
        {
            if (IsInTurn) return;

            selectedMove = null;
            IsInTurn = true;
            var localCompletionSource = turnCompletion = new TaskCompletionSource<Move<TPiece>?>();

            void selectMove(Move<TPiece> move)
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
            if (turnCompletion == null)
                throw new InvalidOperationException("Cannot wait for turn end when this player is not in turn.");

            return turnCompletion.Task;
        }

        protected virtual void OnTurnStart(Action<Move<TPiece>> selectMove) { }

        protected virtual void OnTurnEnd() { }
    }
}
