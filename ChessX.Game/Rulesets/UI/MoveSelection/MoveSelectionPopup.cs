using System;
using System.Collections.Generic;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Graphics;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Rulesets.UI.MoveSelection
{
    public abstract partial class MoveSelectionPopup<TMove> : Popup where TMove : Move
    {
        public Action<TMove> Action { get; set; }

        protected MoveSelectionPopup(IEnumerable<TMove> moves)
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
                    ChildrenEnumerable = CreateButtons(moves)
                }
            });
        }

        protected abstract IEnumerable<Drawable> CreateButtons(IEnumerable<TMove> moves);

        protected abstract partial class PieceButton : ClickableContainer
        {
            [CanBeNull]
            [Resolved(canBeNull: true)]
            private IRotatable rotationParent { get; set; }

            [BackgroundDependencyLoader]
            private void load()
            {
                Size = Vector2.One;
                Alpha = 0.8f;
                Origin = Anchor.Centre;
                Anchor = Anchor.Centre;

                ChildrenEnumerable = CreateContent();
            }

            protected abstract IEnumerable<Drawable> CreateContent();

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
                if (rotationParent != null)
                    Rotation = -rotationParent.Rotation;
            }
        }
    }
}
