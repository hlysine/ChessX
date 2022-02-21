namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class Instruction<TPiece> where TPiece : Piece
    {
        public TPiece Piece { get; }

        protected Instruction(TPiece piece)
        {
            Piece = piece;
        }

        public abstract void Execute(Match<TPiece> match);
    }
}
