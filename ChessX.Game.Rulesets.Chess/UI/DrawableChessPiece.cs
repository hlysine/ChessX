using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public class DrawableChessPiece : DrawablePiece<ChessPiece>
    {
        public DrawableChessPiece(ChessPiece piece)
            : base(piece)
        {
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
        }
    }
}
