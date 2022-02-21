using ChessX.Game.Graphics;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace ChessX.Game.Rulesets.UI
{
    /// <summary>
    /// A drawable that uses grid coordinate positioning and counter-rotates according to parent.
    /// </summary>
    public abstract class GridCoordinateDrawable : CompositeDrawable, IReceiveGridInput
    {
        [CanBeNull]
        [Resolved(canBeNull: true)]
        private IRotatable rotationParent { get; set; }

        protected GridCoordinateDrawable()
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
