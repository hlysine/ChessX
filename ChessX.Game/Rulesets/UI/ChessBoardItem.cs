using ChessX.Game.Graphics;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace ChessX.Game.Rulesets.UI
{
    public abstract class ChessBoardItem : CompositeDrawable, IReceiveGridInput
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private IRotatable rotationParent { get; set; }

        protected ChessBoardItem()
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
            Size = Vector2.One;
        }

        public abstract Vector2I GridPosition { get; }

        protected override void Update()
        {
            base.Update();
            if (rotationParent != null)
                Rotation = -rotationParent.Rotation;
        }
    }
}
