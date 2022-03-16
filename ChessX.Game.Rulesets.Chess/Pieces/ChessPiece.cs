using System.Collections.Generic;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Rulesets.Chess.Moves;

namespace ChessX.Game.Rulesets.Chess.Pieces
{
    public abstract class ChessPiece : Piece
    {
        public ChessColor Color { get; }

        public abstract ChessPieceType PieceType { get; }

        protected ChessPiece(ChessColor color)
        {
            Color = color;
        }

        public override IEnumerable<Move> GetAllowedMoves(IMatch match, bool noRecursion = false)
        {
            // todo: filter by moves that would still leave the player in check
            return GetAllowedMoves((ChessMatch)match, noRecursion);
        }

        public IEnumerable<ChessMove> GetAllowedMoves(ChessMatch match, bool noRecursion = false)
        {
            return GetPossibleMoves(match, noRecursion);
        }

        protected abstract IEnumerable<ChessMove> GetPossibleMoves(ChessMatch match, bool noRecursion);
    }
}
