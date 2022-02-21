using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class PositionalInstruction<TPiece> : Instruction<TPiece> where TPiece : Piece
    {
        public Vector2I Position { get; }

        protected PositionalInstruction(TPiece piece, Vector2I position)
            : base(piece)
        {
            Position = position;
        }
    }
}
