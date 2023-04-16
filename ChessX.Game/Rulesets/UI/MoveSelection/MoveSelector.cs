using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Graphics;
using ChessX.Game.Players;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI.MoveSelection
{
    public abstract class MoveSelector<TPiece> : Container where TPiece : Piece
    {
        [Resolved]
        private PieceContainer pieceContainer { get; set; }

        [Resolved]
        private IMatch match { get; set; }

        public IControllablePlayer<TPiece> Player { get; }

        [Cached(typeof(IPopupContainer))]
        private readonly MoveHintContainer moveHintContainer;

        private DrawablePiece<TPiece> selectedPiece;

        public DrawablePiece<TPiece> SelectedPiece
        {
            get => selectedPiece;
            set
            {
                if (value == selectedPiece)
                    return;

                selectedPiece = value;

                if (value == null)
                    moveHintContainer.Hide();
                else
                {
                    if (Player.IsInTurn)
                        moveHintContainer.Show();
                }

                updateMoveHints();
            }
        }

        protected MoveSelector()
        {
            RelativeSizeAxes = Axes.Both;
            Player = CreatePlayer();
            Add(moveHintContainer = new MoveHintContainer
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(0.5f)
            });
        }

        protected abstract IControllablePlayer<TPiece> CreatePlayer();

        [BackgroundDependencyLoader]
        private void load()
        {
            pieceContainer.PieceClicked += handlePieceClicked;

            Player.TurnStarted += _ =>
            {
                Schedule(() =>
                {
                    moveHintContainer.Show();
                    SelectedPiece = null;
                });
            };

            Player.TurnEnded += () =>
            {
                Schedule(() =>
                {
                    moveHintContainer.Hide();
                    SelectedPiece = null;
                });
            };
        }

        private void handlePieceClicked(DrawablePiece sender, ClickEvent e)
        {
            if (!(sender is DrawablePiece<TPiece> drawablePiece)) return;

            OnPieceClicked(drawablePiece, e);
        }

        protected abstract void OnPieceClicked(DrawablePiece<TPiece> sender, ClickEvent e);

        public void SelectMove(MoveButton<Move<TPiece>> moveButton)
        {
            Player.SelectMove(moveButton.Move);
            Player.EndTurn();
        }

        private void updateMoveHints()
        {
            moveHintContainer.Clear();

            if (selectedPiece == null) return;

            var moves = selectedPiece.Piece.GetAllowedMoves(match).Cast<Move<TPiece>>().GroupBy(m => m.TargetPosition);

            foreach (var moveGroup in moves)
            {
                if (moveGroup.Count() == 1)
                {
                    moveHintContainer.Add(new MoveButton<Move<TPiece>>(moveGroup.First())
                    {
                        Action = SelectMove
                    });
                }
                else
                {
                    moveHintContainer.Add(new CompoundMoveButton<Move<TPiece>>(moveGroup, CreateMoveSelectionPopup)
                    {
                        Action = SelectMove
                    });
                }
            }
        }

        protected abstract MoveSelectionPopup<Move<TPiece>> CreateMoveSelectionPopup(IEnumerable<Move<TPiece>> moves);

        private class MoveHintContainer : VisibilityContainer, IPopupContainer
        {
            protected override void PopIn() => this.FadeIn();

            protected override void PopOut() => this.FadeOut();

            public bool Remove(Drawable drawable)
            {
                return base.Remove(drawable, true);
            }
        }
    }
}
