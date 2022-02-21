using ChessX.Game.Chess.ChessPieces;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class RemoveInstruction : Instruction
    {
        public RemoveInstruction(Piece piece)
            : base(piece)
        {
        }

        public override void Execute(Match match)
        {
            match.Pieces.Remove(Piece);
        }
    }
}
