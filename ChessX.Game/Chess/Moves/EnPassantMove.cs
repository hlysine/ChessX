using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class EnPassantMove : Move
    {
        public override IEnumerable<Instruction> GetInstructions(Match match)
        {
            yield return new MoveInstruction(Piece, TargetPosition);
            yield return new RemoveInstruction(match.GetPieceAt(new Vector2I(TargetPosition.X, Piece.Y)));
        }

        public EnPassantMove(Piece piece, Vector2I targetPosition)
            : base(piece, targetPosition)
        {
        }
    }
}
