using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Input.Events;

namespace ChessX.Game.Rulesets.UI
{
    [Cached]
    public class GridInputRedirector : Container
    {
        public bool SendEvent(Vector2I position, UIEvent e)
        {
            foreach (var receiver in findInputReceivers(this))
            {
                if (receiver.IsPresent && receiver.GridPosition == position)
                {
                    if (receiver.TriggerEvent(e))
                        return true;
                }
            }

            return false;
        }

        private IEnumerable<IReceiveGridInput> findInputReceivers(Container root)
        {
            for (var i = root.Count - 1; i >= 0; i--)
            {
                var child = root[i];

                if (child is IReceiveGridInput receiver)
                    yield return receiver;
                else if (child is Container container)
                {
                    foreach (var n in findInputReceivers(container))
                        yield return n;
                }
            }
        }
    }

    public interface IReceiveGridInput : IDrawable
    {
        public Vector2I GridPosition { get; }

        public bool TriggerEvent(UIEvent e);
    }
}
