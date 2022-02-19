using System;
using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using ChessX.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace ChessX.Game.Chess.Drawables
{
    public class MovePopupDialog : VisibilityContainer
    {
        public Action<Move> Action { get; set; }

        public MovePopupDialog(IEnumerable<Move> moves)
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
            Masking = true;
            CornerRadius = 0.1f;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(40),
                Radius = 0.1f,
            };

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

        protected override void Update()
        {
            base.Update();

            var parentPadding = new MarginPadding(0.5f);

            if (X - OriginPosition.X + DrawWidth > Parent.ChildSize.X + parentPadding.Right)
            {
                X = Parent.ChildSize.X + parentPadding.Right - DrawWidth + OriginPosition.X;
            }

            if (X - OriginPosition.X < -parentPadding.Left)
            {
                X = -parentPadding.Left + OriginPosition.X;
            }

            if (Y - OriginPosition.Y + DrawHeight > Parent.ChildSize.Y + parentPadding.Bottom)
            {
                Y = Parent.ChildSize.Y + parentPadding.Bottom - DrawHeight + OriginPosition.Y;
            }

            if (Y - OriginPosition.Y < -parentPadding.Top)
            {
                Y = -parentPadding.Top + OriginPosition.Y;
            }
        }

        protected override void PopIn() => this.FadeIn(200, Easing.InOutQuint);

        protected override void PopOut() => this.FadeOut(200, Easing.InOutQuint);

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
