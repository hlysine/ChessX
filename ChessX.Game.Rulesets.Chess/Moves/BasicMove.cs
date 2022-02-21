using System.Collections.Generic;
using ChessX.Game.Chess.Moves.Instructions;
using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public class BasicMove : OptionalCaptureMove
    {
        public override IEnumerable<Instruction<ChessPiece>> GetInstructions(ChessMatch match)
        {
            yield return new MoveInstruction<ChessPiece>(Piece, TargetPosition);

            if (!CanCapture) yield break;

            var capture = match.GetPieceAt(TargetPosition);
            if (capture != null)
                yield return new RemoveInstruction<ChessPiece>(capture);
        }

        public BasicMove(ChessPiece piece, Vector2I targetPosition, bool canCapture = true)
            : base(piece, targetPosition, canCapture)
        {
        }
    }
}
