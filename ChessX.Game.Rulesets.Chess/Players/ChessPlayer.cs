using ChessX.Game.Players;
using ChessX.Game.Rulesets.Chess.Pieces;

namespace ChessX.Game.Rulesets.Chess.Players
{
    public abstract class ChessPlayer : Player<ChessPiece>
    {
        public new ChessMatch Match => (ChessMatch)base.Match;

        public ChessColor Color { get; set; }

        public override float TargetBoardRotation => Color == ChessColor.White ? 0 : 180;
    }
}
