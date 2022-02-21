using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces.Utils
{
    public static class ChessPieceMoveUtils
    {
        public static IEnumerable<Move> GenerateMovesForDirection(Piece piece, Match match, Vector2I offset)
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
