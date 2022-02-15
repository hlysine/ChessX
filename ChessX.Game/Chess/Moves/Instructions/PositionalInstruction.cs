using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public abstract class PositionalInstruction : Instruction
    {
        public Vector2I Position { get; }

        protected PositionalInstruction(ChessPiece chessPiece, Vector2I position)
            : base(chessPiece)
        {
            Position = position;
        }
    }
}
