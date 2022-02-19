using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;

namespace ChessX.Game.Chess.Drawables
{
    public class ChessTextureMapper : Component
    {
        [Resolved]
        private TextureStore textures { get; set; }

        public Texture GetChessPiece(ChessPieceType pieceType, ChessColor color)
        {
            return textures.Get($"{pieceType.ToString().ToLowerInvariant()}_{color.ToString().ToLowerInvariant()}");
        }
    }
}
