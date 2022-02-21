using System;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using JetBrains.Annotations;
using osu.Framework.Bindables;

namespace ChessX.Game.Players
{
    public abstract class Player : IPlayer
    {
        public readonly BindableBool IsInTurnBindable = new BindableBool();

        public bool IsInTurn
        {
            get => IsInTurnBindable.Value;
            set => IsInTurnBindable.Value = value;
        }

        public abstract bool RotateBoardInTurn { get; }

        public abstract float TargetBoardRotation { get; }

        public abstract void StartTurn();

        public abstract void EndTurn();

        public abstract Task WaitForTurnEnd();
    }

    public abstract class Player<TPiece> : Player, IPlayer<TPiece> where TPiece : Piece
    {
        public Match<TPiece> Match { get; set; }

        public readonly Bindable<Move<TPiece>> SelectedMoveBindable = new Bindable<Move<TPiece>>();
        private readonly LeasedBindable<Move<TPiece>> selectedMoveBindable;

        public Move<TPiece> SelectedMove => SelectedMoveBindable.Value;

        [CanBeNull]
        private Move<TPiece> selectedMove
        {
            get => selectedMoveBindable.Value;
            set => selectedMoveBindable.Value = value;
        }

        public event IPlayer<TPiece>.TurnStartHandler TurnStarted;

        public event Action TurnEnded;

        private TaskCompletionSource<Move<TPiece>> turnCompletion;

        protected Player()
        {
            selectedMoveBindable = SelectedMoveBindable.BeginLease(false);
        }

        public override void StartTurn()
        {
            if (IsInTurn) return;

            selectedMove = null;
            IsInTurn = true;
            var localCompletionSource = turnCompletion = new TaskCompletionSource<Move<TPiece>>();

            void selectMove(Move<TPiece> move)
            {
                if (localCompletionSource == turnCompletion)
                    selectedMove = move;
            }

            OnTurnStart(selectMove);
            TurnStarted?.Invoke(selectMove);
        }

        public override void EndTurn()
        {
            if (!IsInTurn) return;

            IsInTurn = false;
            turnCompletion?.SetResult(SelectedMove);
            turnCompletion = null;
            OnTurnEnd();
            TurnEnded?.Invoke();
        }

        public override Task WaitForTurnEnd()
        {
            return turnCompletion.Task;
        }

        protected virtual void OnTurnStart(Action<Move<TPiece>> selectMove) { }

        protected virtual void OnTurnEnd() { }
    }
}
