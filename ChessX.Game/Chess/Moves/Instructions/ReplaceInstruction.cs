using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.ChessPieces.Utils;

namespace ChessX.Game.Chess.Moves.Instructions
{
    public class ReplaceInstruction : Instruction
    {
        public ChessPiece NewChessPiece { get; }

        public ReplaceInstruction(ChessPiece chessPiece, ChessPiece newChessPiece)
            : base(chessPiece)
        {
            NewChessPiece = newChessPiece;
        }

        public override void Execute(ChessMatch chessMatch)
        {
            chessMatch.ChessPieces.Remove(ChessPiece);
            chessMatch.ChessPieces.Add(NewChessPiece.WithPosition(ChessPiece.Position));
        }
    }
}
