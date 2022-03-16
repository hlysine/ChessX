using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Players;
using JetBrains.Annotations;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess
{
    public interface IMatch : IHasBoardSize
    {
        public static readonly Vector2I DEFAULT_BOARD_SIZE = new(8);

        Vector2I IHasBoardSize.BoardSize => DEFAULT_BOARD_SIZE;

        public IReadOnlyList<Piece> Pieces { get; }

        public IReadOnlyList<Move> MoveHistory { get; }

        public IReadOnlyList<IPlayer> Players { get; }
    }

    public abstract class Match<TPiece> : IMatch
        where TPiece : Piece
    {
        public virtual Vector2I BoardSize => IMatch.DEFAULT_BOARD_SIZE;

        public BindableList<TPiece> Pieces { get; } = new();

        public BindableList<Move<TPiece>> MoveHistory { get; } = new();

        private readonly List<Player<TPiece>> players = new();
        public IReadOnlyList<Player<TPiece>> Players => players;

        IReadOnlyList<Piece> IMatch.Pieces => Pieces;
        IReadOnlyList<Move> IMatch.MoveHistory => MoveHistory;
        IReadOnlyList<IPlayer> IMatch.Players => players;

        public void AddPlayer(Player<TPiece> player)
        {
            player.Match = this;
            players.Add(player);
        }

        public abstract void Initialize();

        public abstract Task ProcessRound();

        public abstract bool MatchEnded { get; }

        [CanBeNull]
        public TPiece GetPieceAt(Vector2I position)
        {
            return Pieces.FirstOrDefault(p => p.Position == position);
        }

        [CanBeNull]
        public TPiece GetPieceAt(int x, int y) => GetPieceAt(new Vector2I(x, y));

        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < BoardSize.X && y < BoardSize.Y;

        public bool IsInBounds(Vector2I position) => IsInBounds(position.X, position.Y);
    }
}
