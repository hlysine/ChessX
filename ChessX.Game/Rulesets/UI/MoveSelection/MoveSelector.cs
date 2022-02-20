using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Graphics;
using ChessX.Game.Players;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI.MoveSelection
{
    public class MoveSelector : Container
    {
        [Resolved]
        private ChessPieceContainer chessPieceContainer { get; set; }

        [Resolved]
        private ChessMatch chessMatch { get; set; }

        public HumanPlayer Player { get; } = new HumanPlayer();

        private readonly Container content;

        [Cached(typeof(IPopupContainer))]
        private readonly MoveHintContainer moveHintContainer;

        private DrawableChessPiece selectedPiece;

        public DrawableChessPiece SelectedPiece
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
                    moveHintContainer.Show();
                    updateMoveHints();
                }
            }
        }

        public MoveSelector()
        {
            RelativeSizeAxes = Axes.Both;
            Add(content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Child = moveHintContainer = new MoveHintContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(0.5f)
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            chessPieceContainer.PieceClicked += handlePieceClicked;

            Player.TurnStarted += _ =>
            {
                Schedule(() =>
                {
                    content.Show();
                    SelectedPiece = null;
                });
            };

            Player.TurnEnded += () =>
            {
                Schedule(() =>
                {
                    content.Hide();
                    SelectedPiece = null;
                });
            };
        }

        private void handlePieceClicked(DrawableChessPiece sender, ClickEvent e)
        {
            if (sender.ChessPiece.Color != Player.Color) return;

            SelectedPiece = sender;
        }

        public void SelectMove(MoveButton moveButton)
        {
            Player.SelectMove(moveButton.Move);
            Player.EndTurn();
        }

        private void updateMoveHints()
        {
            moveHintContainer.Clear();

            if (selectedPiece == null) return;

            var moves = selectedPiece.ChessPiece.GetAllowedMoves(chessMatch).GroupBy(m => m.TargetPosition);

            foreach (var moveGroup in moves)
            {
                if (moveGroup.Count() == 1)
                {
                    moveHintContainer.Add(new MoveButton(moveGroup.First())
                    {
                        Action = SelectMove
                    });
                }
                else
                {
                    moveHintContainer.Add(new CompoundMoveButton(moveGroup)
                    {
                        Action = SelectMove
                    });
                }
            }
        }

        private class MoveHintContainer : VisibilityContainer, IPopupContainer
        {
            protected override void PopIn() => this.FadeIn(200, Easing.InOutQuint);

            protected override void PopOut() => this.FadeOut(200, Easing.InOutQuint);
        }
    }
}
