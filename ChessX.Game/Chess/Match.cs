using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.ChessPieces.Utils;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Players;
using JetBrains.Annotations;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess
{
    public abstract class Match : IHasBoardSize

    {
        public static readonly Vector2I DEFAULT_BOARD_SIZE = new Vector2I(8);
        public BindableList<Piece> Pieces { get; } = new BindableList<Piece>();

        public BindableList<Move> MoveHistory { get; } = new BindableList<Move>();

        private readonly List<Player> players = new List<Player>();

        public IReadOnlyList<Player> Players => players;

        public virtual Vector2I BoardSize => DEFAULT_BOARD_SIZE;

        protected Match()
        {
        }

        public void AddPlayer(Player player, ChessColor color)
        {
            player.Match = this;
            player.Color = color;
            players.Add(player);
        }

        public virtual void Initialize()
        {
            Pieces.Clear();

            Pieces.AddRange(new[]
            {
                CreatePiece(ChessPieceType.Rook, ChessColor.Black).WithPosition(0, 0),
                CreatePiece(ChessPieceType.Knight, ChessColor.Black).WithPosition(1, 0),
                CreatePiece(ChessPieceType.Bishop, ChessColor.Black).WithPosition(2, 0),
                CreatePiece(ChessPieceType.Queen, ChessColor.Black).WithPosition(3, 0),
                CreatePiece(ChessPieceType.King, ChessColor.Black).WithPosition(4, 0),
                CreatePiece(ChessPieceType.Bishop, ChessColor.Black).WithPosition(5, 0),
                CreatePiece(ChessPieceType.Knight, ChessColor.Black).WithPosition(6, 0),
                CreatePiece(ChessPieceType.Rook, ChessColor.Black).WithPosition(7, 0)
            });
            Pieces.AddRange(Enumerable.Range(0, 8).Select(i => CreatePiece(ChessPieceType.Pawn, ChessColor.Black).WithPosition(i, 1)));

            Pieces.AddRange(new[]
            {
                CreatePiece(ChessPieceType.Rook, ChessColor.White).WithPosition(0, 7),
                CreatePiece(ChessPieceType.Knight, ChessColor.White).WithPosition(1, 7),
                CreatePiece(ChessPieceType.Bishop, ChessColor.White).WithPosition(2, 7),
                CreatePiece(ChessPieceType.Queen, ChessColor.White).WithPosition(3, 7),
                CreatePiece(ChessPieceType.King, ChessColor.White).WithPosition(4, 7),
                CreatePiece(ChessPieceType.Bishop, ChessColor.White).WithPosition(5, 7),
                CreatePiece(ChessPieceType.Knight, ChessColor.White).WithPosition(6, 7),
                CreatePiece(ChessPieceType.Rook, ChessColor.White).WithPosition(7, 7)
            });
            Pieces.AddRange(Enumerable.Range(0, 8).Select(i => CreatePiece(ChessPieceType.Pawn, ChessColor.White).WithPosition(i, 6)));
        }

        public abstract Task ProcessRound();

        public abstract bool MatchEnded { get; }

        public abstract Piece CreatePiece(ChessPieceType type, ChessColor color);

        [NotNull]
        public Player GetPlayerWithColor(ChessColor color)
        {
            return Players.First(p => p.Color == color);
        }

        public Player WhitePlayer => GetPlayerWithColor(ChessColor.White);

        public Player BlackPlayer => GetPlayerWithColor(ChessColor.Black);

        [NotNull]
        public Piece GetKingPiece(ChessColor color)
        {
            return Pieces.First(p => p.Color == color && p.PieceType == ChessPieceType.King);
        }

        [CanBeNull]
        public Piece GetPieceAt(Vector2I position)
        {
            return Pieces.FirstOrDefault(p => p.Position == position);
        }

        [CanBeNull]
        public Piece GetPieceAt(int x, int y) => GetPieceAt(new Vector2I(x, y));

        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < BoardSize.X && y < BoardSize.Y;

        public bool IsInBounds(Vector2I position) => IsInBounds(position.X, position.Y);

        public bool PositionCapturable(Vector2I position, ChessColor capturerColor)
        {
            return Pieces.Where(p => p.Color == capturerColor)
                              .Any(p => p.GetAllowedMoves(this, true).Any(m => m.CanCapture && m.TargetPosition == position));
        }

        public bool IsInCheck(ChessColor color)
        {
            return PositionCapturable(GetKingPiece(color).Position, color.GetOpposingColor());
        }

        public bool HasMovedSinceStart(Piece piece)
        {
            return MoveHistory.Any(h => h.Piece == piece && h.TargetPosition != h.OriginalPosition);
        }

        public bool JustMovedTwoSteps(Piece piece)
        {
            var lastMove = MoveHistory.LastOrDefault(h => h.Piece.Color == piece.Color);

            if (lastMove == null) return false;

            return lastMove.Piece == piece && Math.Abs(lastMove.TargetPosition.Y - lastMove.OriginalPosition.Y) == 2;
        }
    }
}
