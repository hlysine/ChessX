using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.ChessPieces
{
    public class RookPiece : ChessPiece
    {
        public RookPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Rook;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match)
        {
            throw new System.NotImplementedException();
        }
    }
}
