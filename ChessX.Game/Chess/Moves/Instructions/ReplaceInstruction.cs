using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class ReplaceInstruction : Instruction
    {
        public ChessPiece NewChessPiece { get; }

        public ReplaceInstruction(ChessPiece chessPiece, ChessPiece newChessPiece)
            : base(chessPiece)
        {
            NewChessPiece = newChessPiece;
        }
    }
}
