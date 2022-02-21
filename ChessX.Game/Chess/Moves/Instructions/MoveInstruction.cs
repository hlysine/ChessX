using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class MoveInstruction<TPiece> : PositionalInstruction<TPiece> where TPiece : Piece
    {
        public MoveInstruction(TPiece piece, Vector2I position)
            : base(piece, position)
        {
        }

        public override void Execute(Match<TPiece> match)
        {
            Piece.Position = Position;
        }
    }
}
