using System;
using System.Linq;
using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessMatches
{
    public abstract class ChessMatch : IHasBoardSize
    {
        public static readonly Vector2I DEFAULT_BOARD_SIZE = new Vector2I(8);
        public BindableList<ChessPiece> ChessPieces { get; } = new BindableList<ChessPiece>();

        public abstract Vector2I BoardSize { get; }

        public abstract void Initialize();

        public ChessPiece GetKingPiece(ChessColor color)
        {
            return ChessPieces.First(p => p.Color == color && p.PieceType == ChessPieceType.King);
        }

        public ChessPiece GetPieceAt(Vector2I position)
        {
            return ChessPieces.FirstOrDefault(p => p.Position == position);
        }

        public bool IsInCheck(ChessColor color)
        {
            throw new NotImplementedException();
        }
    }
}
