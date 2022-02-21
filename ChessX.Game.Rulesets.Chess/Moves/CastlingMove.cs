using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.Moves.Instructions;
using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public class CastlingMove : ChessMove
    {
        public override IEnumerable<Instruction<ChessPiece>> GetInstructions(ChessMatch match)
        {
            yield return new MoveInstruction<ChessPiece>(Piece, TargetPosition);

            if (Piece.PieceType == ChessPieceType.King)
            {
                if (TargetPosition.X < Piece.X)
                {
                    yield return new MoveInstruction<ChessPiece>(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.Rook && p.X < Piece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction<ChessPiece>(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.Rook && p.X > Piece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
            else
            {
                if (TargetPosition.X < Piece.X)
                {
                    yield return new MoveInstruction<ChessPiece>(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.King && p.X < Piece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction<ChessPiece>(
                        match.Pieces.First(p => p.Color == Piece.Color && p.PieceType == ChessPieceType.King && p.X > Piece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
        }

        public CastlingMove(ChessPiece piece, Vector2I targetPosition)
            : base(piece, targetPosition)
        {
        }
    }
}
