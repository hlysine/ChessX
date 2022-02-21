using System;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Players;
using ChessX.Game.Rulesets.Chess.Pieces;

namespace ChessX.Game.Rulesets.Chess.Players
{
    public class HumanPlayer : ChessPlayer, IControllablePlayer<ChessPiece>
    {
        private Action<Move<ChessPiece>> moveSelector;

        public override bool RotateBoardInTurn => true;

        protected override void OnTurnStart(Action<Move<ChessPiece>> selectMove)
        {
            moveSelector = selectMove;
        }

        protected override void OnTurnEnd()
        {
            moveSelector = null;
        }

        public void SelectMove(Move<ChessPiece> move)
        {
            moveSelector?.Invoke(move);
        }
    }
}
