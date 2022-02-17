using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class RemoveInstruction : Instruction
    {
        public RemoveInstruction(ChessPiece chessPiece)
            : base(chessPiece)
        {
        }
    }
}
