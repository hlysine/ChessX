using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.ChessPieces.Utils;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class ReplaceInstruction : Instruction
    {
        public Piece NewPiece { get; }

        public ReplaceInstruction(Piece piece, Piece newPiece)
            : base(piece)
        {
            NewPiece = newPiece;
        }

        public override void Execute(Match match)
        {
            match.Pieces.Remove(Piece);
            match.Pieces.Add(NewPiece.WithPosition(Piece.Position));
        }
    }
}
