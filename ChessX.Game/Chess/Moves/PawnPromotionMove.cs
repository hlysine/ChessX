using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public class PawnPromotionMove : Move
    {
        public ChessPieceType PromotionChoice { get; }

        public override IEnumerable<Instruction> GetInstructions(ChessMatch chessMatch)
        {
            yield return new MoveInstruction(ChessPiece, TargetPosition);

            var newChessPiece = chessMatch.CreateChessPiece(PromotionChoice, ChessPiece.Color);
            newChessPiece.Position = TargetPosition;
            yield return new ReplaceInstruction(ChessPiece, newChessPiece);
        }

        public PawnPromotionMove(ChessPiece chessPiece, Vector2I targetPosition, ChessPieceType promotionChoice)
            : base(chessPiece, targetPosition)
        {
            PromotionChoice = promotionChoice;
        }
    }
}
