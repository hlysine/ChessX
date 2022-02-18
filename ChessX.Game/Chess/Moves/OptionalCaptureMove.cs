using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public abstract class OptionalCaptureMove : Move
    {
        public override bool CanCapture { get; }

        protected OptionalCaptureMove(ChessPiece chessPiece, Vector2I targetPosition, bool canCapture = true)
            : base(chessPiece, targetPosition)
        {
            CanCapture = canCapture;
        }
    }
}
