using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.ChessPieces
{
    public class KnightPiece : ChessPiece
    {
        public KnightPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Knight;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match)
        {
            throw new System.NotImplementedException();
        }
    }
}
