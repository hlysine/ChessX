using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class MoveInstruction : PositionalInstruction
    {
        public MoveInstruction(Piece piece, Vector2I position)
            : base(piece, position)
        {
        }

        public override void Execute(Match match)
        {
            Piece.Position = Position;
        }
    }
}
