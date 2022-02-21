using System.Collections.Generic;
using ChessX.Game.Chess.Moves.Instructions;
using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public class EnPassantMove : ChessMove
    {
        public override IEnumerable<Instruction<ChessPiece>> GetInstructions(ChessMatch match)
        {
            yield return new MoveInstruction<ChessPiece>(Piece, TargetPosition);
            yield return new RemoveInstruction<ChessPiece>(match.GetPieceAt(new Vector2I(TargetPosition.X, Piece.Y)));
        }

        public EnPassantMove(ChessPiece piece, Vector2I targetPosition)
            : base(piece, targetPosition)
        {
        }
    }
}
