using System;
using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Chess.Utils;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public class KingPiece : StatefulChessPiece
    {
        public KingPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.King;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match)
        {
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i == X && j == Y) continue;
                    if (!match.IsInBounds(i, j)) continue;
                    if (match.GetPieceAt(i, j)?.Color == Color) continue;
                    if (match.PositionCapturable(new Vector2I(i, j), Color.GetOpposingColor())) continue;

                    yield return new BasicMove(this, new Vector2I(i, j));
                }
            }

            // Castling
            if (HasMovedSinceStart) yield break;

            if (match.IsInCheck(Color)) yield break;

            var rooks = match.ChessPieces.Where(p => p.Color == Color && p.PieceType == ChessPieceType.Rook)
                             .Cast<RookPiece>()
                             .Where(p => !p.HasMovedSinceStart);

            if (!rooks.Any()) yield break;

            foreach (var rook in rooks)
            {
                if (match.ChessPieces.Any(p => p.Position.Y == Y && MathUtils.InBetween(p.Position.X, rook.X, X)))
                    yield break;

                var targetPos = new Vector2I(X + Math.Sign(rook.X - X) * 2, Y);
                var middlePos = new Vector2I((X + targetPos.X) / 2, Y);

                if (match.PositionCapturable(targetPos, Color.GetOpposingColor()) || match.PositionCapturable(middlePos, Color.GetOpposingColor()))
                    continue;

                yield return new CastlingMove(this, targetPos);
            }
        }
    }
}
