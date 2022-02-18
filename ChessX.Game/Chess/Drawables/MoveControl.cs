using ChessX.Game.Chess.Players;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace ChessX.Game.Chess.Drawables
{
    public class MoveControl : Container
    {
        [Resolved]
        private ChessPieceContainer chessPieceContainer { get; set; }

        [Resolved]
        private ChessMatch chessMatch { get; set; }

        public HumanPlayer Player { get; } = new HumanPlayer();

        private readonly Container content;

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

        public MoveControl()
        {
            RelativeSizeAxes = Axes.Both;
            Add(content = new Container
            {
                Child = moveHintContainer = new MoveHintContainer { RelativeSizeAxes = Axes.Both }
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

        private void updateMoveHints()
        {
            moveHintContainer.Clear();

            if (selectedPiece == null) return;

            var moves = selectedPiece.ChessPiece.GetAllowedMoves(chessMatch);

            foreach (var move in moves)
            {
                moveHintContainer.Add(new MoveHint(move)
                {
                    Action = hint =>
                    {
                        Player.SelectMove(hint.Move);
                        Player.EndTurn();
                    }
                });
            }
        }

        private class MoveHintContainer : VisibilityContainer
        {
            protected override void PopIn() => this.FadeIn(200, Easing.InOutQuint);

            protected override void PopOut() => this.FadeOut(200, Easing.InOutQuint);
        }
    }
}
