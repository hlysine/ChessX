using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class PositionalInstruction : Instruction
    {
        public Vector2I Position { get; }

        protected PositionalInstruction(Piece piece, Vector2I position)
            : base(piece)
        {
            Position = position;
        }
    }
}
