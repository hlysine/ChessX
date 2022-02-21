using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class BasicMove : OptionalCaptureMove
    {
        public override IEnumerable<Instruction> GetInstructions(Match match)
        {
            yield return new MoveInstruction(Piece, TargetPosition);

            if (!CanCapture) yield break;

            var capture = match.GetPieceAt(TargetPosition);
            if (capture != null)
                yield return new RemoveInstruction(capture);
        }

        public BasicMove(Piece piece, Vector2I targetPosition, bool canCapture = true)
            : base(piece, targetPosition, canCapture)
        {
        }
    }
}
