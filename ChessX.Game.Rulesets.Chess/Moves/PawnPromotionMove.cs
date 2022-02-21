using System.Collections.Generic;
using ChessX.Game.Chess.Moves.Instructions;
using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public class PawnPromotionMove : OptionalCaptureMove
    {
        public ChessPieceType PromotionChoice { get; }

        public override IEnumerable<Instruction<ChessPiece>> GetInstructions(ChessMatch match)
        {
            yield return new MoveInstruction<ChessPiece>(Piece, TargetPosition);

            var newChessPiece = match.CreatePiece(PromotionChoice, Piece.Color);
            newChessPiece.Position = TargetPosition;
            yield return new ReplaceInstruction<ChessPiece>(Piece, newChessPiece);

            if (!CanCapture) yield break;

            var capture = match.GetPieceAt(TargetPosition);
            if (capture != null)
                yield return new RemoveInstruction<ChessPiece>(capture);
        }

        public PawnPromotionMove(ChessPiece piece, Vector2I targetPosition, ChessPieceType promotionChoice, bool canCapture)
            : base(piece, targetPosition, canCapture)
        {
            PromotionChoice = promotionChoice;
        }
    }
}
