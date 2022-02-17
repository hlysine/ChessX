using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class AddInstruction : PositionalInstruction
    {
        public AddInstruction(ChessPiece chessPiece, Vector2I position)
            : base(chessPiece, position)
        {
        }
    }
}
