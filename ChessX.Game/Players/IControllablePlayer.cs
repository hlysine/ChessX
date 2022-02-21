using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Players
{
    public interface IControllablePlayer<TPiece> : IPlayer<TPiece> where TPiece : Piece
    {
        void SelectMove(Move<TPiece> move);
    }
}
