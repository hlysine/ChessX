using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class CastlingMove : Move
    {
        public override MoveType Type => MoveType.Castling;

        public override IEnumerable<Instruction> GetInstructions(ChessMatch chessMatch)
        {
            yield return new MoveInstruction(ChessPiece, TargetPosition);

            if (ChessPiece.PieceType == ChessPieceType.King)
            {
                if (TargetPosition.X < ChessPiece.X)
                {
                    yield return new MoveInstruction(
                        chessMatch.ChessPieces.First(p => p.Color == ChessPiece.Color && p.PieceType == ChessPieceType.Rook && p.X < ChessPiece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction(
                        chessMatch.ChessPieces.First(p => p.Color == ChessPiece.Color && p.PieceType == ChessPieceType.Rook && p.X > ChessPiece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
            else
            {
                if (TargetPosition.X < ChessPiece.X)
                {
                    yield return new MoveInstruction(
                        chessMatch.ChessPieces.First(p => p.Color == ChessPiece.Color && p.PieceType == ChessPieceType.King && p.X < ChessPiece.X),
                        new Vector2I(TargetPosition.X + 1, TargetPosition.Y)
                    );
                }
                else
                {
                    yield return new MoveInstruction(
                        chessMatch.ChessPieces.First(p => p.Color == ChessPiece.Color && p.PieceType == ChessPieceType.King && p.X > ChessPiece.X),
                        new Vector2I(TargetPosition.X - 1, TargetPosition.Y)
                    );
                }
            }
        }

        public CastlingMove(ChessPiece chessPiece, Vector2I targetPosition)
            : base(chessPiece, targetPosition)
        {
        }
    }
}
