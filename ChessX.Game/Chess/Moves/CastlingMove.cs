using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class CastlingMove : Move
    {
        public override IEnumerable<Instruction> GetInstructions(Match match)
        {
            yield return new MoveInstruction(Piece, TargetPosition);

            if (Piece.PieceType == ChessPieceType.King)
            {
                if (TargetPosition.X < Piece.X)
                {
                    yield return new MoveInstruction(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.Rook && p.X < Piece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.Rook && p.X > Piece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
            else
            {
                if (TargetPosition.X < Piece.X)
                {
                    yield return new MoveInstruction(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.King && p.X < Piece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.King && p.X > Piece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
        }

        public CastlingMove(Piece piece, Vector2I targetPosition)
            : base(piece, targetPosition)
        {
        }
    }
}
