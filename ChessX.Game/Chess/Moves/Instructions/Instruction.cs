using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class Instruction
    {
        public abstract InstructionType Type { get; }

        public ChessPiece ChessPiece { get; }

        protected Instruction(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
        }
    }
}
