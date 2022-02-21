namespace ChessX.Game.Chess.Moves.Instructions
{
    public class ReplaceInstruction<TPiece> : Instruction<TPiece> where TPiece : Piece
    {
        public TPiece NewPiece { get; }

        public ReplaceInstruction(TPiece piece, TPiece newPiece)
            : base(piece)
        {
            NewPiece = newPiece;
        }

        public override void Execute(Match<TPiece> match)
        {
            match.Pieces.Remove(Piece);
            match.Pieces.Add(NewPiece.WithPosition(Piece.Position));
        }
    }
}
