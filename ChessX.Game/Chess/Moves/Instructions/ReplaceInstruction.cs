using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class ReplaceInstruction : Instruction
    {
        public override InstructionType Type => InstructionType.Replace;

        public ChessPiece NewChessPiece { get; }

        public ReplaceInstruction(ChessPiece chessPiece, ChessPiece newChessPiece)
            : base(chessPiece)
        {
            NewChessPiece = newChessPiece;
        }
    }
}
