using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class MoveInstruction : PositionalInstruction
    {
        public MoveInstruction(ChessPiece chessPiece, Vector2I position)
            : base(chessPiece, position)
        {
        }

        public override void Execute(ChessMatch chessMatch)
        {
            ChessPiece.Position = Position;
        }
    }
}
