using ChessX.Game.Chess;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace ChessX.Game.Rulesets.UI
{
    public class ChessPieceSprite : Sprite
    {
        [Resolved]
        private TextureStore textures { get; set; }

        public readonly Bindable<ChessPieceType> PieceTypeBindable = new Bindable<ChessPieceType>();

        public ChessPieceType PieceType
        {
            get => PieceTypeBindable.Value;
            set => PieceTypeBindable.Value = value;
        }

        public readonly Bindable<ChessColor> ColorBindable = new Bindable<ChessColor>();

        public ChessColor Color
        {
            get => ColorBindable.Value;
            set => ColorBindable.Value = value;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            PieceTypeBindable.BindValueChanged(e => updateTexture(), true);
            ColorBindable.BindValueChanged(e => updateTexture(), true);
        }

        private void updateTexture()
        {
            Texture = getChessPiece(PieceType, Color);
        }

        private Texture getChessPiece(ChessPieceType pieceType, ChessColor color)
        {
            return textures.Get($"{pieceType.ToString().ToLowerInvariant()}_{color.ToString().ToLowerInvariant()}");
        }
    }
}
