using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class PawnPromotionMove : OptionalCaptureMove
    {
        public ChessPieceType PromotionChoice { get; }

        public override IEnumerable<Instruction> GetInstructions(Match match)
        {
            yield return new MoveInstruction(Piece, TargetPosition);

            var newChessPiece = match.CreatePiece(PromotionChoice, Piece.Color);
            newChessPiece.Position = TargetPosition;
            yield return new ReplaceInstruction(Piece, newChessPiece);

            if (!CanCapture) yield break;

            var capture = match.GetPieceAt(TargetPosition);
            if (capture != null)
                yield return new RemoveInstruction(capture);
        }

        public PawnPromotionMove(Piece piece, Vector2I targetPosition, ChessPieceType promotionChoice, bool canCapture)
            : base(piece, targetPosition, canCapture)
        {
            PromotionChoice = promotionChoice;
        }
    }
}
