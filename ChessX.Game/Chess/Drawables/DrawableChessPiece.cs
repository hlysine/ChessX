using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.UserInterface;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public class DrawableChessPiece : CompositeDrawable
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private IRotatable rotationParent { get; set; }

        public ChessPiece ChessPiece { get; set; }

        public DrawableChessPiece(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;

            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
            Size = Vector2.One;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get(GetTextureName())
            });
        }

        protected string GetTextureName()
        {
            return $"{ChessPiece.PieceType.ToString().ToLowerInvariant()}_{ChessPiece.Color.ToString().ToLowerInvariant()}";
        }

        protected override void Update()
        {
            base.Update();
            if (rotationParent != null)
                Rotation = -rotationParent.Rotation;
        }
    }
}
