using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class BasicMove : Move
    {
        public override MoveType Type => MoveType.Move;

        public override IEnumerable<Instruction> GetInstructions(ChessMatch chessMatch)
        {
            yield return new MoveInstruction(ChessPiece, TargetPosition);

            var capture = chessMatch.GetPieceAt(TargetPosition);
            if (capture != null)
                yield return new RemoveInstruction(capture);
        }

        public BasicMove(ChessPiece chessPiece, Vector2I targetPosition)
            : base(chessPiece, targetPosition)
        {
        }
    }
}
