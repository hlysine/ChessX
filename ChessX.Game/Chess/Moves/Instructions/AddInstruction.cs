using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class AddInstruction<TPiece> : PositionalInstruction<TPiece> where TPiece : Piece
    {
        public AddInstruction(TPiece piece, Vector2I position)
            : base(piece, position)
        {
        }

        public override void Execute(Match<TPiece> match)
        {
            match.Pieces.Add(Piece.WithPosition(Position));
        }
    }
}
