using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osuTK;
using osuTK.Graphics;

namespace ChessX.Game.Graphics
{
    public class Popup : VisibilityContainer
    {
        [Resolved(canBeNull: true)]
        private IDialogContainer dialogContainer { get; set; }

        public Popup()
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 0.1f;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(40),
                Radius = 0.1f,
            };
        }

        protected override void Update()
        {
            base.Update();

            if (dialogContainer == null)
                return;

            var containerPadding = dialogContainer.Padding;

            if (X - OriginPosition.X + DrawWidth > dialogContainer.ChildSize.X + containerPadding.Right)
            {
                X = dialogContainer.ChildSize.X + containerPadding.Right - DrawWidth + OriginPosition.X;
            }

            if (X - OriginPosition.X < -containerPadding.Left)
            {
                X = -containerPadding.Left + OriginPosition.X;
            }

            if (Y - OriginPosition.Y + DrawHeight > dialogContainer.ChildSize.Y + containerPadding.Bottom)
            {
                Y = dialogContainer.ChildSize.Y + containerPadding.Bottom - DrawHeight + OriginPosition.Y;
            }

            if (Y - OriginPosition.Y < -containerPadding.Top)
            {
                Y = -containerPadding.Top + OriginPosition.Y;
            }
        }

        protected override void PopIn() => this.FadeIn(200, Easing.InOutQuint);

        protected override void PopOut() => this.FadeOut(200, Easing.InOutQuint);
    }

    public interface IDialogContainer : IContainerEnumerable<Drawable>, IContainerCollection<Drawable>, IReadOnlyList<Drawable>
    {
        public MarginPadding Padding { get; }

        public Vector2 ChildSize { get; }
    }
}
