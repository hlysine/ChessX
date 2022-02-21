using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Rulesets.Chess.Moves;
using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.UI.MoveSelection;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;

namespace ChessX.Game.Rulesets.Chess.UI.MoveSelection
{
    public class ChessMoveSelectionPopup : MoveSelectionPopup<Move<ChessPiece>>
    {
        public ChessMoveSelectionPopup(IEnumerable<Move<ChessPiece>> moves)
            : base(moves)
        {
        }

        protected override IEnumerable<Drawable> CreateButtons(IEnumerable<Move<ChessPiece>> moves)
        {
            foreach (var move in moves)
            {
                ChessPieceType pieceType;

                if (move is PawnPromotionMove pawnPromotion)
                    pieceType = pawnPromotion.PromotionChoice;
                else
                    pieceType = move.Piece.PieceType;

                yield return new ChessPieceButton(pieceType, move.Piece.Color)
                {
                    Action = () =>
                    {
                        Hide();
                        Action?.Invoke(move);
                    }
                };
            }
        }

        protected class ChessPieceButton : PieceButton
        {
            public ChessPieceType PieceType { get; }

            public ChessColor Color { get; }

            public ChessPieceButton(ChessPieceType pieceType, ChessColor color)
            {
                PieceType = pieceType;
                Color = color;
            }

            protected override IEnumerable<Drawable> CreateContent() => new ChessPieceSprite
            {
                RelativeSizeAxes = Axes.Both,
                PieceType = PieceType,
                Color = Color
            }.Yield();
        }
    }
}
