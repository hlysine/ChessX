using System;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.Players
{
    public class HumanPlayer : Player
    {
        private Action<Move> moveSelector;

        protected override void OnTurnStart(Action<Move> selectMove)
        {
            moveSelector = selectMove;
        }

        protected override void OnTurnEnd()
        {
            moveSelector = null;
        }

        public void SelectMove(Move move)
        {
            moveSelector?.Invoke(move);
        }
    }
}
