using System;
using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Rulesets.Chess.Moves;
using ChessX.Game.Utils;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Pieces
{
    public class KingPiece : ChessPiece
    {
        public KingPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.King;

        protected override IEnumerable<ChessMove> GetPossibleMoves(ChessMatch match, bool noRecursion)
        {
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i == X && j == Y) continue;
                    if (!match.IsInBounds(i, j)) continue;
                    if (match.GetPieceAt(i, j)?.Color == Color) continue;
                    if (!noRecursion && match.PositionCapturable(new Vector2I(i, j), Color.GetOpposingColor())) continue;

                    yield return new BasicMove(this, new Vector2I(i, j));
                }
            }

            // Castling
            if (noRecursion) yield break;

            if (match.HasMovedSinceStart(this)) yield break;

            if (match.IsInCheck(Color)) yield break;

            var rooks = match.Pieces.Where(p => p.Color == Color && p.PieceType == ChessPieceType.Rook)
                             .Cast<RookPiece>()
                             .Where(p => !match.HasMovedSinceStart(p));

            if (!rooks.Any()) yield break;

            foreach (var rook in rooks)
            {
                if (match.Pieces.Any(p => p.Position.Y == Y && MathUtils.InBetween(p.Position.X, rook.X, X)))
                    continue;

                var targetPos = new Vector2I(X + Math.Sign(rook.X - X) * 2, Y);
                var middlePos = new Vector2I((X + targetPos.X) / 2, Y);

                if (match.PositionCapturable(targetPos, Color.GetOpposingColor()) || match.PositionCapturable(middlePos, Color.GetOpposingColor()))
                    continue;

                yield return new CastlingMove(this, targetPos);
            }
        }
    }
}
