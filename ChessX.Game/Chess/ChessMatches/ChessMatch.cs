using System.Linq;
using ChessX.Game.Chess.ChessPieces;
using JetBrains.Annotations;
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

        [NotNull]
        public ChessPiece GetKingPiece(ChessColor color)
        {
            return ChessPieces.First(p => p.Color == color && p.PieceType == ChessPieceType.King);
        }

        [CanBeNull]
        public ChessPiece GetPieceAt(Vector2I position)
        {
            return ChessPieces.FirstOrDefault(p => p.Position == position);
        }

        [CanBeNull]
        public ChessPiece GetPieceAt(int x, int y) => GetPieceAt(new Vector2I(x, y));

        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < BoardSize.X && y < BoardSize.Y;

        public bool IsInBounds(Vector2I position) => IsInBounds(position.X, position.Y);

        public bool PositionCapturable(Vector2I position, ChessColor capturerColor)
        {
            return ChessPieces.Where(p => p.Color == capturerColor)
                              .Any(p => p.GetAllowedMoves(this).Any(m => m.CanCaptureTarget && m.TargetPosition == position));
        }

        public bool IsInCheck(ChessColor color)
        {
            return PositionCapturable(GetKingPiece(color).Position, color.GetOpposingColor());
        }
    }
}
