using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.UI;
using osu.Framework.Graphics;

namespace ChessX.Game.Rulesets.Chess.UI
{
    public class DrawableChessMatch : DrawableMatch<ChessPiece>
    {
        public DrawableChessMatch(ChessMatch match)
            : base(match)
        {
        }

        protected override Drawable CreateGameBoard() => new Checkerboard
        {
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
            RelativeSizeAxes = Axes.Both
        };

        protected override DrawablePiece<ChessPiece> CreateDrawableRepresentation(ChessPiece piece) => new DrawableChessPiece(piece);
    }
}
