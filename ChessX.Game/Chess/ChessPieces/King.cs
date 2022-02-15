namespace ChessX.Game.Chess.ChessPieces
{
    public class KingPiece : ChessPiece
    {
        public KingPiece(ChessColor color)
            : base(color)
        {
        }

        public override ChessPieceType PieceType => ChessPieceType.King;
    }
}
