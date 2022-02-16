using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.ChessPieces
{
    public class PawnPiece : StatefulChessPiece
    {
        public PawnPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Pawn;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match)
        {
            throw new System.NotImplementedException();
        }
    }
}
