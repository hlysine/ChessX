using System.Collections.Generic;
using ChessX.Game.Rulesets.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Pieces
{
    public class RookPiece : ChessPiece
    {
        public RookPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Rook;

        protected override IEnumerable<ChessMove> GetPossibleMoves(ChessMatch match, bool noRecursion)
        {
            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(1, 0)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(-1, 0)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(0, 1)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(0, -1)))
                yield return move;
        }
    }
}
