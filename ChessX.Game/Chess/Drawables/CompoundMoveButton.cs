using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.Moves;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public class CompoundMoveButton : MoveButton
    {
        private readonly List<Move> moves;

        private Move selectedMove;
        public override Move Move => selectedMove;

        public override Vector2I GridPosition => Move.TargetPosition;

        public CompoundMoveButton(IEnumerable<Move> move)
            : base(move.First())
        {
            moves = move.ToList();
            AddInternal(new SpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Size = new Vector2(0.4f),
                Colour = Colour4.White,
                Icon = FontAwesome.Solid.EllipsisH
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            // todo: show dialog
            selectedMove = moves.First();
            Action.Invoke(this);
            return true;
        }
    }
}
