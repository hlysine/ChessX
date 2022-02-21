using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public abstract class OptionalCaptureMove : Move
    {
        public override bool CanCapture { get; }

        protected OptionalCaptureMove(Piece piece, Vector2I targetPosition, bool canCapture = true)
            : base(piece, targetPosition)
        {
            CanCapture = canCapture;
        }
    }
}
