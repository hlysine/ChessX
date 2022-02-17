using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class Instruction
    {
        public ChessPiece ChessPiece { get; }

        protected Instruction(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
        }

        public abstract void Execute(ChessMatch chessMatch);
    }
}
