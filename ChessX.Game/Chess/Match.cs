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
    public abstract class Match : IHasBoardSize
    {
        public static readonly Vector2I DEFAULT_BOARD_SIZE = new Vector2I(8);

        public virtual Vector2I BoardSize => DEFAULT_BOARD_SIZE;
    }

    public abstract class Match<TPiece> : Match
        where TPiece : Piece
    {
        public BindableList<TPiece> Pieces { get; } = new BindableList<TPiece>();

        public BindableList<Move<TPiece>> MoveHistory { get; } = new BindableList<Move<TPiece>>();

        private readonly List<Player<TPiece>> players = new List<Player<TPiece>>();

        public IReadOnlyList<Player<TPiece>> Players => players;

        protected Match()
        {
        }

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
