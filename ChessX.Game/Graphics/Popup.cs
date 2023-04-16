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
    public partial class Popup : VisibilityContainer
    {
        [Resolved(canBeNull: true)]
        private IPopupContainer popupContainer { get; set; }

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

            if (popupContainer == null)
                return;

            var containerPadding = popupContainer.Padding;

            if (X - OriginPosition.X + DrawWidth > popupContainer.ChildSize.X + containerPadding.Right)
            {
                X = popupContainer.ChildSize.X + containerPadding.Right - DrawWidth + OriginPosition.X;
            }

            if (X - OriginPosition.X < -containerPadding.Left)
            {
                X = -containerPadding.Left + OriginPosition.X;
            }

            if (Y - OriginPosition.Y + DrawHeight > popupContainer.ChildSize.Y + containerPadding.Bottom)
            {
                Y = popupContainer.ChildSize.Y + containerPadding.Bottom - DrawHeight + OriginPosition.Y;
            }

            if (Y - OriginPosition.Y < -containerPadding.Top)
            {
                Y = -containerPadding.Top + OriginPosition.Y;
            }
        }

        protected override void PopIn() => this.FadeIn(200, Easing.InOutQuint);

        protected override void PopOut() => this.FadeOut(200, Easing.InOutQuint);
    }

    public interface IPopupContainer : IPopupContainer<Drawable>
    {
    }

    public interface IPopupContainer<T> : IContainerEnumerable<T>, IContainerCollection<T>, ICollection<T>, IReadOnlyList<T>
        where T : Drawable
    {
        MarginPadding Padding { get; }

        Vector2 ChildSize { get; }

        new void Add(T drawable);

        new IReadOnlyList<T> Children { get; set; }

        new bool Remove(T drawable);

        new int Count { get; }
    }
}
