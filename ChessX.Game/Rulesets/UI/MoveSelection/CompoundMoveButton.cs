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
    public partial class CompoundMoveButton<TMove> : MoveButton<TMove> where TMove : Move
    {
        [Resolved]
        private IPopupContainer popupContainer { get; set; }

        private readonly List<TMove> moves;

        private readonly PopupCreator popupCreator;

        private MoveSelectionPopup<TMove> selectionPopup;

        private TMove selectedMove;
        public override TMove Move => selectedMove;

        public override Vector2I GridPosition => Move.TargetPosition;

        public delegate MoveSelectionPopup<TMove> PopupCreator(IEnumerable<TMove> moves);

        public CompoundMoveButton(IEnumerable<TMove> move, PopupCreator popupCreator)
            : base(move.First())
        {
            this.popupCreator = popupCreator;
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
            foreach (var child in popupContainer)
            {
                if (child is MoveSelectionPopup<TMove> popupDialog)
                    popupDialog.Hide();
            }

            if (selectionPopup != null)
            {
                selectionPopup.Show();
                return true;
            }

            selectionPopup = popupCreator(moves).With(d => d.Position = GridPosition);
            selectionPopup.Action = move =>
            {
                selectedMove = move;
                Action.Invoke(this);
            };
            LoadComponentAsync(selectionPopup, d =>
            {
                popupContainer.Add(d);
                d.Show();
            });
            return true;
        }
    }
}
