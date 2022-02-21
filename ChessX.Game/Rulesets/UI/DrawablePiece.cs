using ChessX.Game.Chess.ChessPieces;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class DrawablePiece : ChessBoardItem
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private ChessPieceContainer chessPieceContainer { get; set; }

        public Piece Piece { get; }

        private readonly Bindable<Vector2I> positionBindable = new Bindable<Vector2I>();

        public override Vector2I GridPosition => Piece.Position;

        protected DrawablePiece(Piece piece)
        {
            Piece = piece;
        }

        protected virtual void OnPositionChanged(ValueChangedEvent<Vector2I> e)
        {
            this.MoveTo(e.NewValue, 200, Easing.InOutQuint);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new ChessPieceSprite
            {
                RelativeSizeAxes = Axes.Both,
                PieceType = Piece.PieceType,
                Color = Piece.Color
            });

            positionBindable.BindTo(Piece.PositionBindable);
            positionBindable.BindValueChanged(e => Schedule(() => OnPositionChanged(e)), true);
            FinishTransforms(true);
        }

        protected override bool OnClick(ClickEvent e)
        {
            chessPieceContainer?.TriggerPieceClick(this, e);
            return true;
        }
    }
}
