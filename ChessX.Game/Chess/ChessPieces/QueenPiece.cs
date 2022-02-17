using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces.Utils;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public class QueenPiece : ChessPiece
    {
        public QueenPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Queen;

        protected override IEnumerable<Move> GetPossibleMoves(ChessMatch match, bool noRecursion)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(i, j)))
                        yield return move;
                }
            }
        }
    }
}
