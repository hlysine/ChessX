using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class Instruction
    {
        public Piece Piece { get; }

        protected Instruction(Piece piece)
        {
            Piece = piece;
        }

        public abstract void Execute(Match match);
    }
}
