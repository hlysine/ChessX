using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class Instruction
    {
        public abstract InstructionType Type { get; }

        public ChessPiece ChessPiece { get; set; }
    }
}
