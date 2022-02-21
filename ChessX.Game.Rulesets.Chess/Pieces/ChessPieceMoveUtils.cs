using System.Collections.Generic;
using ChessX.Game.Rulesets.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Pieces
{
    public static class ChessPieceMoveUtils
    {
        public static IEnumerable<ChessMove> GenerateMovesForDirection(ChessPiece piece, ChessMatch match, Vector2I offset)
        {
            var currentPos = piece.Position + offset;

            while (match.IsInBounds(currentPos))
            {
                var currentPiece = match.GetPieceAt(currentPos);

                if (currentPiece != null)
                {
                    if (currentPiece.Color.IsOppositeOf(piece.Color))
                        yield return new BasicMove(piece, currentPos);

                    yield break;
                }

                yield return new BasicMove(piece, currentPos);

                currentPos += offset;
            }
        }
    }
}
