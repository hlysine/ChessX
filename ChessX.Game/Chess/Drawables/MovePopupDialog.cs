using System;
using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using ChessX.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public class MovePopupDialog : Dialog
    {
        public Action<Move> Action { get; set; }

        public MovePopupDialog(IEnumerable<Move> moves)
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;

            AddRange(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.LightGray
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Margin = new MarginPadding(0.1f),
                    ChildrenEnumerable = createButtons(moves)
                }
            });
        }

        private IEnumerable<Drawable> createButtons(IEnumerable<Move> moves)
        {
            foreach (var move in moves)
            {
                ChessPieceType pieceType;

                if (move is PawnPromotionMove pawnPromotion)
                    pieceType = pawnPromotion.PromotionChoice;
                else
                    pieceType = move.ChessPiece.PieceType;

                yield return new ChessPieceButton(pieceType, move.ChessPiece.Color)
                {
                    Action = () =>
                    {
                        Hide();
                        Action?.Invoke(move);
                    }
                };
            }
        }

        private class ChessPieceButton : ClickableContainer
        {
            public ChessPieceType PieceType { get; }

            public ChessColor Color { get; }

            [Resolved(canBeNull: true)]
            private IRotatable rotationParent { get; set; }

            [Resolved]
            private ChessTextureMapper chessTextureMapper { get; set; }

            public ChessPieceButton(ChessPieceType pieceType, ChessColor color)
            {
                PieceType = pieceType;
                Color = color;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Size = Vector2.One;
                Alpha = 0.8f;
                Origin = Anchor.Centre;
                Anchor = Anchor.Centre;

                Child = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Texture = chessTextureMapper.GetChessPiece(PieceType, Color)
                };
            }

            protected override bool OnHover(HoverEvent e)
            {
                this.FadeIn(100, Easing.InOutQuint);
                return true;
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                this.FadeTo(0.8f, 100, Easing.InOutQuint);
            }

            protected override void Update()
            {
                base.Update();
                Rotation = -rotationParent.Rotation;
            }
        }
    }
}
