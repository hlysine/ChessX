using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces.Utils;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public class BishopPiece : Piece
    {
        public BishopPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.Bishop;

        protected override IEnumerable<Move> GetPossibleMoves(Match match, bool noRecursion)
        {
            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(1, 1)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(1, -1)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(-1, 1)))
                yield return move;

            foreach (var move in ChessPieceMoveUtils.GenerateMovesForDirection(this, match, new Vector2I(-1, -1)))
                yield return move;
        }
    }
}
