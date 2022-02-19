using System;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Utils;

namespace ChessX.Game.Players
{
    public class AIPlayer : Player
    {
        public override bool RotateChessBoard => false;

        protected override void OnTurnStart(Action<Move> selectMove)
        {
            Task.Run(() =>
            {
                selectMove(ChessMatch.ChessPieces.Where(p => p.Color == Color).SelectMany(p => p.GetAllowedMoves(ChessMatch)).GetRandom());
                EndTurn();
            });
        }
    }
}
