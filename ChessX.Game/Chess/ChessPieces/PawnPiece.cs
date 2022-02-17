using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public class PawnPiece : ChessPiece
    {
        private static readonly ChessPieceType[] promotion_choices =
        {
            ChessPieceType.Queen,
            ChessPieceType.Bishop,
            ChessPieceType.Knight,
            ChessPieceType.Rook
        };

        public PawnPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Pawn;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match)
        {
            // Moving forward
            int forwardOffset = Color == ChessColor.Black ? 1 : -1;
            int opponentBaseRank = Color == ChessColor.Black ? match.BoardSize.Y - 1 : 0;

            if (match.IsInBounds(X, Y + forwardOffset) && match.GetPieceAt(X, Y + forwardOffset) == null)
            {
                var targetPos = new Vector2I(X, Y + forwardOffset);

                if (Y + forwardOffset == opponentBaseRank)
                {
                    // Pawn Promotion
                    foreach (var choice in promotion_choices)
                        yield return new PawnPromotionMove(this, targetPos, choice);
                }
                else
                {
                    yield return new BasicMove(this, targetPos);
                }

                // Move two steps at the start
                if (!match.HasMovedSinceStart(this))
                {
                    if (match.IsInBounds(X, Y + forwardOffset * 2) && match.GetPieceAt(X, Y + forwardOffset * 2) == null)
                        yield return new BasicMove(this, new Vector2I(X, Y + forwardOffset * 2));
                }
            }

            // Capturing
            if (canCapture(match, X - 1, Y + forwardOffset))
                yield return new BasicMove(this, new Vector2I(X - 1, Y + forwardOffset));

            if (canCapture(match, X + 1, Y + forwardOffset))
                yield return new BasicMove(this, new Vector2I(X + 1, Y + forwardOffset));

            // En Passant
            if (canEnPassant(match, X - 1, Y))
                yield return new EnPassantMove(this, new Vector2I(X - 1, Y + forwardOffset));

            if (canEnPassant(match, X + 1, Y))
                yield return new EnPassantMove(this, new Vector2I(X + 1, Y + forwardOffset));
        }

        private bool canCapture(ChessMatch match, int x, int y)
        {
            if (!match.IsInBounds(x, y)) return false;

            var targetPiece = match.GetPieceAt(x, y);
            return targetPiece != null && targetPiece.Color.IsOppositeOf(Color);
        }

        private bool canEnPassant(ChessMatch match, int x, int y)
        {
            if (!match.IsInBounds(x, y)) return false;

            var targetPiece = match.GetPieceAt(x, y);
            return targetPiece != null
                   && targetPiece.Color.IsOppositeOf(Color)
                   && targetPiece.PieceType == ChessPieceType.Pawn
                   && match.JustMovedTwoSteps(targetPiece);
        }
    }
}
