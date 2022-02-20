using System.Collections.Generic;
using System.Linq;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace ChessX.Game.Rulesets.UI.MoveSelection
{
    public class CompoundMoveButton : MoveButton
    {
        [Resolved]
        private IDialogContainer dialogContainer { get; set; }

        private readonly List<Move> moves;

        private MoveSelectionPopup selectionPopup;

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
                if (child is MoveSelectionPopup popupDialog)
                    popupDialog.Hide();
            }

            if (selectionPopup != null)
            {
                selectionPopup.Show();
                return true;
            }

            selectionPopup = new MoveSelectionPopup(moves)
            {
                Position = GridPosition
            };
            selectionPopup.Action = move =>
            {
                selectedMove = move;
                Action.Invoke(this);
            };
            LoadComponentAsync(selectionPopup, d =>
            {
                dialogContainer.Add(d);
                d.Show();
            });
            return true;
        }
    }
}
