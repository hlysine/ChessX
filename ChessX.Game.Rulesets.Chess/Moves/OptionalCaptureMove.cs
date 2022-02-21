using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public abstract class OptionalCaptureMove : ChessMove
    {
        public override bool CanCapture { get; }

        protected OptionalCaptureMove(ChessPiece piece, Vector2I targetPosition, bool canCapture = true)
            : base(piece, targetPosition)
        {
            CanCapture = canCapture;
        }
    }
}
