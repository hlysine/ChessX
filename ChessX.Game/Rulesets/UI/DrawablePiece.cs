using ChessX.Game.Chess;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class DrawablePiece : GridCoordinateDrawable
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private PieceContainer pieceContainer { get; set; }

        protected override bool OnClick(ClickEvent e)
        {
            pieceContainer?.TriggerPieceClick(this, e);
            return true;
        }
    }

    public abstract class DrawablePiece<TPiece> : DrawablePiece where TPiece : Piece
    {
        public TPiece Piece { get; }

        private readonly Bindable<Vector2I> positionBindable = new Bindable<Vector2I>();

        public override Vector2I GridPosition => Piece.Position;

        protected DrawablePiece(TPiece piece)
        {
            Piece = piece;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            positionBindable.BindTo(Piece.PositionBindable);
            positionBindable.BindValueChanged(e => Schedule(() => OnPositionChanged(e)), true);
            FinishTransforms(true);
        }

        protected virtual void OnPositionChanged(ValueChangedEvent<Vector2I> e)
        {
            this.MoveTo(e.NewValue, 200, Easing.InOutQuint);
        }
    }
}
