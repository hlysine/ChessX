using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.Moves;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    public class CompoundMoveButton : MoveButton
    {
        [Resolved]
        private Container dialogContainer { get; set; }

        private readonly List<Move> moves;

        private MovePopupDialog dialog;

        private Move selectedMove;
        public override Move Move => selectedMove;

        public override Vector2I GridPosition => Move.TargetPosition;

        public CompoundMoveButton(IEnumerable<Move> move)
            : base(move.First())
        {
            moves = move.ToList();
            selectedMove = moves.First();
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
            foreach (var child in dialogContainer)
            {
                if (child is MovePopupDialog popupDialog)
                    popupDialog.Hide();
            }

            if (dialog != null)
            {
                dialog.Show();
                return true;
            }

            dialog = new MovePopupDialog(moves)
            {
                Position = GridPosition
            };
            dialog.Action = move =>
            {
                selectedMove = move;
                Action.Invoke(this);
            };
            LoadComponentAsync(dialog, d =>
            {
                dialogContainer.Add(d);
                d.Show();
            });
            return true;
        }
    }
}
