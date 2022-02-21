namespace ChessX.Game.Chess.Moves.Instructions
{
    public class RemoveInstruction<TPiece> : Instruction<TPiece> where TPiece : Piece
    {
        public RemoveInstruction(TPiece piece)
            : base(piece)
        {
        }

        public override void Execute(Match<TPiece> match)
        {
            match.Pieces.Remove(Piece);
        }
    }
}
