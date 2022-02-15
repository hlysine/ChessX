using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class CaptureMove : Move
    {
        public override MoveType Type => MoveType.Capture;

        public override IEnumerable<Instruction> GetInstructions(ChessMatch chessMatch)
        {
            yield return new MoveInstruction(ChessPiece, TargetPosition);
            yield return new RemoveInstruction(chessMatch.GetPieceAt(TargetPosition));
        }

        public CaptureMove(ChessPiece chessPiece, Vector2I targetPosition)
            : base(chessPiece, targetPosition)
        {
        }
    }
}
