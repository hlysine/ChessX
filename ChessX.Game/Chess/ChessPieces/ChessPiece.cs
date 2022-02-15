using System;
using System.Collections.Generic;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.Moves;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessPieces
{
    public abstract class ChessPiece
    {
        public ChessColor Color { get; }
        public abstract ChessPieceType PieceType { get; }

        public readonly Bindable<Vector2I> PositionBindable = new Bindable<Vector2I>();

        public Vector2I Position
        {
            get => PositionBindable.Value;
            set => PositionBindable.Value = value;
        }

        public int X
        {
            get => Position.X;
            set => Position = new Vector2I(value, Position.Y);
        }

        public int Y
        {
            get => Position.Y;
            set => Position = new Vector2I(Position.X, value);
        }

        protected ChessPiece(ChessColor color)
        {
            Color = color;
        }

        public static ChessPiece CreateChessPiece(ChessPieceType pieceType, ChessColor color) => pieceType switch
        {
            ChessPieceType.King => new KingPiece(color),
            ChessPieceType.Queen => new QueenPiece(color),
            ChessPieceType.Bishop => new BishopPiece(color),
            ChessPieceType.Knight => new KnightPiece(color),
            ChessPieceType.Rook => new RookPiece(color),
            ChessPieceType.Pawn => new PawnPiece(color),
            _ => throw new ArgumentOutOfRangeException(nameof(pieceType), pieceType, null)
        };

        public IEnumerable<Move> GetAllowedMoves(ChessMatch match)
        {
            throw new NotImplementedException();
        }

        protected abstract IEnumerable<Move> GetPossibleMoves(ChessMatch match);
    }
}
