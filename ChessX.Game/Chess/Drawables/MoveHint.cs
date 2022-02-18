using System;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public class MoveHint : ChessBoardItem
    {
        public Move Move { get; }

        public Action<MoveHint> Action { get; set; }

        public override Vector2I GridPosition => Move.TargetPosition;

        public MoveHint(Move move)
        {
            Move = move;
            Position = move.TargetPosition;
            Size = Vector2.Zero;

            AddInternal(new Circle
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(0.5f),
                Colour = Colour4.Gray,
                Alpha = 0.5f
            });
        }

        protected override void LoadAsyncComplete()
        {
            base.LoadAsyncComplete();
            this.ResizeTo(Vector2.One, 200, Easing.InOutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Action.Invoke(this);
            return true;
        }
    }
}
