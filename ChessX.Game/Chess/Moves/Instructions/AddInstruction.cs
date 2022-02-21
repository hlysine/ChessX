using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.ChessPieces.Utils;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class AddInstruction : PositionalInstruction
    {
        public AddInstruction(Piece piece, Vector2I position)
            : base(piece, position)
        {
        }

        public override void Execute(Match match)
        {
            match.Pieces.Add(Piece.WithPosition(Position));
        }
    }
}
