using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces.Utils
{
    public static class ChessPieceMoveUtils
    {
        public static IEnumerable<Move> GenerateMovesForDirection(ChessPiece chessPiece, ChessMatch match, Vector2I offset)
        {
            var currentPos = chessPiece.Position + offset;

            while (match.IsInBounds(currentPos))
            {
                var currentPiece = match.GetPieceAt(currentPos);

                if (currentPiece != null)
                {
                    if (currentPiece.Color.IsOppositeOf(chessPiece.Color))
                        yield return new BasicMove(chessPiece, currentPos);

                    yield break;
                }

                yield return new BasicMove(chessPiece, currentPos);

                currentPos += offset;
            }
        }
    }
}
