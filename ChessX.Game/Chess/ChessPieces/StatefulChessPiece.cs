namespace ChessX.Game.Chess.ChessPieces
{
    public abstract class StatefulChessPiece : ChessPiece
    {
        public bool HasMovedSinceStart { get; set; }

        protected StatefulChessPiece(ChessColor color)
            : base(color)
        {
        }
    }
}
