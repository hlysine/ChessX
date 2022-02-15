using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class RemoveInstruction : Instruction
    {
        public override InstructionType Type => InstructionType.Remove;

        public RemoveInstruction(ChessPiece chessPiece)
            : base(chessPiece)
        {
        }
    }
}
