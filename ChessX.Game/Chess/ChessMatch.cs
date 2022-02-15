using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Bindables;

namespace ChessX.Game.Chess
{
    public class ChessMatch
    {
        public IBindableList<ChessPiece> ChessPieces { get; } = new BindableList<ChessPiece>();
    }
}
