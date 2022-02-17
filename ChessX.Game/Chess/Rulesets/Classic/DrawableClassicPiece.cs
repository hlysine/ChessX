using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Drawables;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class DrawableClassicPiece : DrawableChessPiece
    {
        public DrawableClassicPiece(ChessPiece chessPiece)
            : base(chessPiece)
        {
        }

        protected override void OnPositionChanged(ValueChangedEvent<Vector2I> e)
        {
            this.MoveTo(e.NewValue, 200, Easing.InOutQuint);
        }
    }
}
