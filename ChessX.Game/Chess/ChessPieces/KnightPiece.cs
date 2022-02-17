using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public class KnightPiece : ChessPiece
    {
        private static readonly Vector2I[] possible_offsets =
        {
            new Vector2I(2, 1),
            new Vector2I(2, -1),
            new Vector2I(-2, 1),
            new Vector2I(-2, -1),
            new Vector2I(1, 2),
            new Vector2I(-1, 2),
            new Vector2I(1, -2),
            new Vector2I(-1, -2)
        };

        public KnightPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Knight;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match, bool noRecursion)
        {
            foreach (var offset in possible_offsets)
            {
                var targetPos = Position + offset;

                if (!match.IsInBounds(targetPos)) continue;

                var targetPiece = match.GetPieceAt(targetPos);

                if (targetPiece == null || targetPiece.Color.IsOppositeOf(Color))
                    yield return new BasicMove(this, targetPos);
            }
        }
    }
}
