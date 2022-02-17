using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class EnPassantMove : Move
    {
        public override IEnumerable<Instruction> GetInstructions(ChessMatch chessMatch)
        {
            yield return new MoveInstruction(ChessPiece, TargetPosition);
            yield return new RemoveInstruction(chessMatch.GetPieceAt(new Vector2I(TargetPosition.X, ChessPiece.Y)));
        }

        public EnPassantMove(ChessPiece chessPiece, Vector2I targetPosition)
            : base(chessPiece, targetPosition)
        {
        }
    }
}
