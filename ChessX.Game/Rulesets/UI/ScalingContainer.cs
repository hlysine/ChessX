using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Rulesets.UI
{
    /// <summary>
    /// A <see cref="Container"/> which scales its content relative to a target width and height.
    /// </summary>
    public class ScalingContainer : Container
    {
        public int TargetWidth { get; set; }

        public int TargetHeight { get; set; }

        protected override void Update()
        {
            base.Update();

            Scale = new Vector2(Parent.ChildSize.X / TargetWidth, Parent.ChildSize.Y / TargetHeight);
            Size = Vector2.Divide(Vector2.One, Scale);
        }
    }
}
