using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Players;
using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.Chess.Players;
using ChessX.Game.Rulesets.UI;
using ChessX.Game.Rulesets.UI.MoveSelection;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.Chess.UI.MoveSelection
{
    public class ChessMoveSelector : MoveSelector<ChessPiece>
    {
        public new HumanPlayer Player => (HumanPlayer)base.Player;

        protected override IControllablePlayer<ChessPiece> CreatePlayer() => new HumanPlayer();

        protected override void OnPieceClicked(DrawablePiece<ChessPiece> sender, ClickEvent e)
        {
            if (sender.Piece.Color != Player.Color) return;

            SelectedPiece = sender;
        }

        protected override MoveSelectionPopup<Move<ChessPiece>> CreateMoveSelectionPopup(IEnumerable<Move<ChessPiece>> moves)
            => new ChessMoveSelectionPopup(moves);
    }
}
