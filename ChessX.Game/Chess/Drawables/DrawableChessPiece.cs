using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.UserInterface;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;

namespace ChessX.Game.Chess.Drawables
{
    public abstract class DrawableChessPiece : ChessBoardItem
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private IRotatable rotationParent { get; set; }

        [CanBeNull]
        [Resolved(canBeNull: true)]
        private ChessPieceContainer chessPieceContainer { get; set; }

        [Resolved]
        private ChessTextureMapper chessTextureMapper { get; set; }

        public ChessPiece ChessPiece { get; }

        private readonly Bindable<Vector2I> positionBindable = new Bindable<Vector2I>();

        public override Vector2I GridPosition => ChessPiece.Position;

        protected DrawableChessPiece(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
        }

        protected virtual void OnPositionChanged(ValueChangedEvent<Vector2I> e)
        {
            this.MoveTo(e.NewValue, 200, Easing.InOutQuint);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = GetTexture()
            });

            positionBindable.BindTo(ChessPiece.PositionBindable);
            positionBindable.BindValueChanged(e => Schedule(() => OnPositionChanged(e)), true);
            FinishTransforms(true);
        }

        protected Texture GetTexture()
        {
            return chessTextureMapper.GetChessPiece(ChessPiece.PieceType, ChessPiece.Color);
        }

        protected override void Update()
        {
            base.Update();
            if (rotationParent != null)
                Rotation = -rotationParent.Rotation;
        }

        protected override bool OnClick(ClickEvent e)
        {
            chessPieceContainer?.TriggerPieceClick(this, e);
            return true;
        }
    }
}
