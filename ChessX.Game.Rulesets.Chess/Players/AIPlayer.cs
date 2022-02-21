using System;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Utils;

namespace ChessX.Game.Rulesets.Chess.Players
{
    public class AIPlayer : ChessPlayer
    {
        public override bool RotateBoardInTurn => false;

        protected override void OnTurnStart(Action<Move<ChessPiece>> selectMove)
        {
            Task.Run(() =>
            {
                selectMove(Match.Pieces.Where(p => p.Color == Color).SelectMany(p => p.GetAllowedMoves(Match)).GetRandom());
                EndTurn();
            });
        }
    }
}
